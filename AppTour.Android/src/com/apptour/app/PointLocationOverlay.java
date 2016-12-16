package com.apptour.app;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.drawable.Drawable;

import com.google.android.maps.GeoPoint;
import com.google.android.maps.ItemizedOverlay;
import com.google.android.maps.OverlayItem;
import com.apptour.framework.PointModel;

import java.util.ArrayList;
import java.util.List;

public class PointLocationOverlay extends ItemizedOverlay<OverlayItem>  {

	   private List<PointModel> points;
	   private Context context;

	   public PointLocationOverlay(Context context, List<PointModel> points, Drawable marker) {
		  super(boundCenterBottom(marker));
	      this.context = context;
	      this.points = points;
	      if (points == null) {
	         points = new ArrayList<PointModel>();
	      }
	      populate();
	   }

	   @Override
	   protected OverlayItem createItem(int i) {
	      PointModel point = points.get(i);
	      // GeoPoint uses lat/long in microdegrees format (1e6)
	      GeoPoint geopoint =
	               new GeoPoint((int) (point.getLatitude() * 1e6), (int) (point.getLongitude() * 1e6));
	      return new OverlayItem(geopoint, point.getName(), null);
	   }

	   @Override
	   public boolean onTap(final int index) {
	      PointModel point = points.get(index);
	      AlertDialog.Builder builder = new AlertDialog.Builder(context);
	      builder.setTitle(R.string.point_title)
	               .setMessage(point.getName() + "\n" + context.getString(R.string.point_visit)).setCancelable(true)
	               .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
	                  public void onClick(DialogInterface dialog, int id) {
	                     Intent i = new Intent(context, PointDetailActivity.class);
	                     i.putExtra("POINT_ID", points.get(index).getId());
	                     context.startActivity(i);
	                  }
	               }).setNegativeButton(R.string.button_no, new DialogInterface.OnClickListener() {
	                  public void onClick(DialogInterface dialog, int id) {
	                     dialog.cancel();
	                  }
	               });
	      AlertDialog alert = builder.create();
	      alert.show();

	      return true; // we'll handle the event here (true) not pass to another overlay (false)
	   }

	   @Override
	   public int size() {
	      return points.size();
	   }
}
