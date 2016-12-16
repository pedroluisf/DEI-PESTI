package com.apptour.app;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.widget.Gallery;
import android.widget.TextView;
import android.widget.Toast;

import com.apptour.framework.PointModel;
import com.apptour.framework.WSPoint;

public class PointDetailActivity extends Activity {

	ProgressDialog progressDialog;
	PointModel myPoint;
	boolean loaded;
	TextView tvName;
	TextView tvAddress;
	Gallery galPic;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.pointdetail);

		// Display my Point Information
		myPoint = new PointModel();
		myPoint.setId(getIntent().getExtras().getString("POINT_ID"));
		loaded = false;
		DownloadTask call = new DownloadTask();
		call.execute(null);
		
		//getViews
		tvName = (TextView) findViewById(R.id.point_name);
		tvAddress = (TextView) findViewById(R.id.point_address);
		galPic = (Gallery) findViewById(R.id.point_Pictures);
	}

	@Override
	protected void onResume() {
		// TODO Auto-generated method stub
		super.onResume();
		if (!loaded) {
			progressDialog = ProgressDialog.show(this, "",
					getString(R.string.loading));
		}
	}

	private void fillPointInfo() {
		tvName.setText(myPoint.getName());
		tvAddress.setText(myPoint.getAdress());
	}

	private class DownloadTask extends AsyncTask<Void, Boolean, Boolean> {

		@Override
		protected Boolean doInBackground(Void... params) {

			WSPoint ws = new WSPoint();
			PointModel point = ws.getPoint(myPoint.getId());
			if (point != null) {
				myPoint = point;
			}
			return null;

		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			progressDialog.dismiss();
			loaded = true;
			fillPointInfo();
		}

	}

}
