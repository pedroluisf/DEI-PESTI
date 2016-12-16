package com.apptour.framework;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

import com.google.android.maps.GeoPoint;

public class WSSearch extends WebService {

	public List<PointModel> doSearch(String ISO2, String userID, String searchProfileID, GeoPoint location){
		List<PointModel> myPoints = new ArrayList<PointModel>();
		
		//Build Location as expected on Server
		StringBuilder loc = new StringBuilder();
		loc.append(location.getLatitudeE6() / 1E6);
		loc.append(".");
		loc.append(location.getLongitudeE6() / 1E6);
		
		HashMap<String, String> params = new HashMap<String, String>();
		params.put("ISO2", ISO2);
		params.put("UserId", userID);
		params.put("SearchProfileId", searchProfileID);
		params.put("Location", loc.toString());

		try {

			String res = performRequest("Search", params);

			if (res != null) {
				
				JSONArray myJsonArray = new JSONArray(res);
				for (int i = 0; i < myJsonArray.length(); i++) {
	
					JSONObject row = myJsonArray.getJSONObject(i);

				    PointModel newPoint = new PointModel();
				    newPoint.setId(row.getString("Id"));
				    newPoint.setName(row.getString("Name"));
				    newPoint.setAdress(row.getString("Address"));
				    newPoint.setPostalCode(row.getString("PostalCode"));
				    newPoint.setPhoneNumber(row.getString("PhoneNumber"));
				    newPoint.setUrl(row.getString("URL"));
				    newPoint.setTopicId(row.getString("TopicId"));
			    	
				    String [] coordenates = row.getString("Coordenate").split(",");
				    if (coordenates.length==2){
					    newPoint.setLatitude(Double.parseDouble(coordenates[0]));
					    newPoint.setLongitude(Double.parseDouble(coordenates[1]));
				    } else if (coordenates.length==4) {
					    newPoint.setLatitude(Double.parseDouble(coordenates[0] + "." + coordenates[1]));
					    newPoint.setLongitude(Double.parseDouble(coordenates[2] + "." + coordenates[3]));
				    } else {
				    	break;
				    }
				    
				    myPoints.add(newPoint);
				    
				}
								

			}

		} catch (Exception e) {

			Log.e("WSSearch", e.getMessage());

		}

		return myPoints;
	}
	
}
