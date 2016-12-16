package com.apptour.app;

import java.util.ArrayList;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.widget.Toast;

import com.google.android.maps.GeoPoint;
import com.google.android.maps.ItemizedOverlay;
import com.google.android.maps.MapView;
import com.google.android.maps.OverlayItem;

public class Markers extends ItemizedOverlay {
	private Context context;

	 private ArrayList<OverlayItem> mOverlays = new ArrayList<OverlayItem>();

	 public Markers(Drawable defaultMarker, Context context) {

	      super(boundCenterBottom(defaultMarker));
	      this.context = context;
	      // TODO Auto-generated constructor stub
	 }

	 @Override
	 protected OverlayItem createItem(int i) {
	      // TODO Auto-generated method stub
	      return mOverlays.get(i);
	 }

	 @Override
	 public boolean onTap(GeoPoint p, MapView mapView) {
	      // TODO Auto-generated method stub
	      return super.onTap(p, mapView);
	 }


	 @Override
	 protected boolean onTap(int index) {
	      // TODO Auto-generated method stub
	      //Toast.makeText(this.context, mOverlays.get(index).getTitle().toString()+", Latitude: "+mOverlays.get(index).getPoint().getLatitudeE6(), Toast.LENGTH_SHORT).show();
	      return super.onTap(index);         
	 }

	 @Override
	 public int size() {
	      // TODO Auto-generated method stub
	      return mOverlays.size();
	 }

	 public void addOverlay(OverlayItem item) {
	      mOverlays.add(item);
	      setLastFocusedIndex(-1);
	      populate();

	 }

	 public void clear() {
	      mOverlays.clear();
	      setLastFocusedIndex(-1);
	      populate();
	 }

}
