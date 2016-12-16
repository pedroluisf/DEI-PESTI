package com.apptour.app;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.text.SpannableString;
import android.text.method.LinkMovementMethod;
import android.text.util.Linkify;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.apptour.framework.ApplicationValues;
import com.apptour.framework.AuthenticationHelper;
import com.apptour.framework.LocationHelper;
import com.apptour.framework.StringUtils;
import com.apptour.framework.ThemeModel;
import com.apptour.framework.TopicModel;
import com.apptour.framework.WSInitializer;

public class AppTourActivity extends Activity {

	private boolean mustInitialize;
	private SpannableString aboutString;
	private static final int MENU_ABOUT = 1;
	ApplicationValues myValues;
	ProgressDialog progressDialog;
	TextView tvTap;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main_screen);

		// Set Main image
		ImageView image = (ImageView) findViewById(R.id.main_image);
		try {
			InputStream bitmap = getAssets().open("apptour_full_logo.png");
			Bitmap bit = BitmapFactory.decodeStream(bitmap);
			image.setImageBitmap(bit);
		} catch (Exception e) {
			Log.e("AppTourActivity", "Error Loading Splash Image");
		}

		// Tap text
		tvTap = (TextView) findViewById(R.id.tv_main_tap);
		// Tap Image Listener
		ImageView img = (ImageView) findViewById(R.id.main_image);
		img.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				if (myValues.getUser_id() != null) {
					map();
				} else {
					validateSession();
				}

			}
		});

		// About us
		aboutString = new SpannableString(getString(R.string.about_apptour));
		Pattern pattern = Pattern.compile("WebSite");
		String scheme = "http://apptour.dei.isep.ipp.pt/apptour";
		Linkify.addLinks(aboutString, pattern, scheme);

		// Get my Singleton Containers
		myValues = ApplicationValues.getInstance(this);

		// flag to initialize Application
		mustInitialize = true;
	}

	@Override
	public void onResume() {
		super.onResume();
		if (mustInitialize) {
			if (!doWeHaveInternet()) {
				Toast.makeText(AppTourActivity.this,
						R.string.no_internet_connection, Toast.LENGTH_SHORT)
						.show();
				return;
			}
			initializeApp();
			mustInitialize = false;

			validateSession();

		}
	}

	private void validateSession() {

		// Do we have user?
		if (StringUtils.GUID_EMPTY.equals(myValues.getUser_id())) {

			AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(
					AppTourActivity.this);

			// set title
			alertDialogBuilder.setTitle(getString(R.string.session_missing));

			// set dialog message
			alertDialogBuilder
					.setMessage(getString(R.string.session_required))
					.setCancelable(false)
					.setPositiveButton(getString(R.string.button_login),
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id) {
									dialog.cancel();
									login();
								}
							})
					.setNegativeButton(getString(R.string.button_register),
							new DialogInterface.OnClickListener() {
								public void onClick(DialogInterface dialog,
										int id) {
									dialog.cancel();
									register();
								}
							});

			// create alert dialog
			AlertDialog alertDialog = alertDialogBuilder.create();

			// show it
			alertDialog.show();
		}

	}

	private void login() {

		AuthenticationHelper auth = new AuthenticationHelper(this);
		auth.login();

	}

	private void register() {

		Intent i = new Intent(getApplication(), RegisterActivity.class);
		startActivity(i);

	}

	private void map() {
		Intent i = new Intent(getApplication(), AppTourMapActivity.class);
		startActivity(i);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		menu.add(0, MENU_ABOUT, 0, "About").setIcon(
				android.R.drawable.ic_menu_info_details);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case MENU_ABOUT:
			AlertDialog dialog = new AlertDialog.Builder(AppTourActivity.this)
					.setTitle(R.string.menu_about)
					.setMessage(aboutString)
					.setNeutralButton("Ok",
							new DialogInterface.OnClickListener() {
								public void onClick(final DialogInterface d,
										final int i) {
								}
							}).create();
			dialog.show();
			// make the Linkify'ed aboutString clickable
			((TextView) dialog.findViewById(android.R.id.message))
					.setMovementMethod(LinkMovementMethod.getInstance());
			break;
		}
		return false;
	}

	@SuppressWarnings("static-access")
	private void initializeApp() {

		progressDialog = ProgressDialog.show(this, "",
				getString(R.string.initializing));

		// Check Location Availability and store it in Current Settings
		Handler handler = new Handler() {
			public void handleMessage(Message m) {
				if (m.what == LocationHelper.MESSAGE_CODE_LOCATION_FOUND) {
					myValues.setCurrentLocation(m.arg1, m.arg2);
				}
			}
		};
		LocationManager locationMgr = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
		LocationHelper helper = new LocationHelper(locationMgr, handler);
		helper.getCurrentLocation(30);

		// Populate All Themes and topics from Central Repository
		WSInitializer wsInit = new WSInitializer();
		List<ThemeModel> listThemes = wsInit.getAllThemes(myValues.getISO2());
		List<TopicModel> listTopics = wsInit.getAllTopics(myValues.getISO2());

		for (TopicModel topic : listTopics) {
			for (ThemeModel theme : listThemes) {
				if (theme.getId().equals(topic.getThemeId())) {
					theme.topics.add(topic);
					break;
				}
			}
		}
		myValues.setThemes(listThemes);

		progressDialog.dismiss();

	}

	private boolean doWeHaveInternet() {
		ConnectivityManager conMgr = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);

		if (conMgr.getNetworkInfo(0).getState() == NetworkInfo.State.CONNECTED
				|| conMgr.getNetworkInfo(1).getState() == NetworkInfo.State.CONNECTING) {
			return true;
		} else if (conMgr.getNetworkInfo(0).getState() == NetworkInfo.State.DISCONNECTED
				|| conMgr.getNetworkInfo(1).getState() == NetworkInfo.State.DISCONNECTED) {
			return false;
		} else {
			return true;
		}
	}

}