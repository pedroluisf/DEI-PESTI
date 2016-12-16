package com.apptour.app;

import java.util.ArrayList;
import java.util.List;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.location.LocationManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

import com.apptour.framework.ApplicationValues;
import com.apptour.framework.LocationHelper;
import com.apptour.framework.PointModel;
import com.apptour.framework.ThemeModel;
import com.apptour.framework.TopicModel;
import com.apptour.framework.WSSearch;
import com.google.android.maps.MapActivity;
import com.google.android.maps.MapView;
import com.google.android.maps.Overlay;

public class AppTourMapActivity extends MapActivity {

	private MapView map;
	private List<Overlay> overlays;
	private Markers myMarkers;
	private ApplicationValues myValues;
	ProgressDialog progressDialog;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.map);

		myValues = ApplicationValues.getInstance(this);
		
		map = (MapView) findViewById(R.id.myMapView);
		map.displayZoomControls(true);
		map.setBuiltInZoomControls(true);

		// Center Map
		map.getController().setCenter(myValues.getCurrentLocation());
		map.getController().setZoom(16);

		showPoints();

	}

	@Override
	protected boolean isRouteDisplayed() {
		// TODO Auto-generated method stub
		return false;
	}

	public void setLocation(View view) {

		progressDialog = ProgressDialog.show(this, "",
				getString(R.string.loading));

		// Check Location Availability and store it in Current Settings
		Handler handler = new Handler() {
			public void handleMessage(Message m) {
				if (m.what == LocationHelper.MESSAGE_CODE_LOCATION_FOUND) {
					myValues.setCurrentLocation(m.arg1, m.arg2);
					map = (MapView) findViewById(R.id.myMapView);
					map.getController()
							.setCenter(myValues.getCurrentLocation());
					// map.getController().setZoom(16);
					progressDialog.dismiss();
				} else if (m.what == LocationHelper.MESSAGE_CODE_LOCATION_NULL) {
					Toast.makeText(AppTourMapActivity.this,
							R.string.unable_get_location, Toast.LENGTH_SHORT)
							.show();
					progressDialog.dismiss();
				} else if (m.what == LocationHelper.MESSAGE_CODE_PROVIDER_NOT_PRESENT) {
					Log.i("Location",
							getString(R.string.location_provider_non_existent));
					Toast.makeText(AppTourMapActivity.this,
							R.string.location_provider_non_existent,
							Toast.LENGTH_SHORT).show();
					progressDialog.dismiss();
				}
			}
		};

		LocationManager locationMgr = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
		LocationHelper helper = new LocationHelper(locationMgr, handler);
		helper.getCurrentLocation(30);

	}

	public void setFilters(View view) {
		startActivity(new Intent(getApplication(), AppTourSearchProfile.class));
	}

	public void doSearch(View view) {
		progressDialog = ProgressDialog.show(this, "",
				getString(R.string.loading));

		DownloadTask call = new DownloadTask();
		call.execute(null);
		
	}

	private void showPoints() {

		overlays = map.getOverlays();
		int myTopicIconId = 0;

		for (ThemeModel theme : myValues.getThemes()) {
			for (TopicModel topic : theme.topics) {
				ArrayList<PointModel> points = new ArrayList<PointModel>();
				myTopicIconId = this.getResources().getIdentifier(
						topic.getIconName(), "drawable", getPackageName());
				for (PointModel point : myValues.getPoints()) {
					if (point.getTopicId().equalsIgnoreCase(topic.getId())) {
						points.add(point);
					}
				}
				PointLocationOverlay pointLocationOverlay = new PointLocationOverlay(
						this, points, this.getResources().getDrawable(myTopicIconId));
				overlays.add(pointLocationOverlay);
			}
		}

		// zoom to the span if we have points to show
		// if (myValues.getPoints().size() > 0) {
		// map.getController().zoomToSpan(pointLocationOverlay.getLatSpanE6(),
		// pointLocationOverlay.getLonSpanE6());
		// }
	}

	private class DownloadTask extends AsyncTask<Void, Boolean, Boolean> {

		@Override
		protected Boolean doInBackground(Void... params) {
			
			WSSearch ws = new WSSearch();
			myValues.setPoints(ws.doSearch(myValues.getISO2(),
					myValues.getUser_id(), myValues.getSearchProfileId(),
					myValues.getCurrentLocation()));

			return null;
			
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			progressDialog.dismiss();
			showPoints();
			map.invalidate();
		}
		
	}
	
}
