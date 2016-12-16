package com.apptour.framework;

import java.util.HashMap;

import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

public class WSPoint extends WebService {

	public PointModel getPoint(String pointId){
		
		PointModel newPoint = null;

		HashMap<String, String> params = new HashMap<String, String>();
		params.put("PointId", pointId);

		try {

			String res = performRequest("getPoint", params);

			if (res != null) {
				
				JSONObject resPoint = new JSONObject(res);

			    newPoint = new PointModel();
			    newPoint.setId(resPoint.getString("Id"));
			    newPoint.setName(resPoint.getString("Name"));
			    newPoint.setAdress(resPoint.getString("Address"));
			    newPoint.setPostalCode(resPoint.getString("PostalCode"));
			    newPoint.setPhoneNumber(resPoint.getString("PhoneNumber"));
			    newPoint.setUrl(resPoint.getString("URL"));
		    	
				JSONArray myTopics = resPoint.getJSONArray("Topics");
				JSONObject topic = myTopics.getJSONObject(0);
			    newPoint.setTopicId(topic.getString("Id"));
				
				JSONArray myPics = resPoint.getJSONArray("Pictures");
				for (int i = 0 ; i < myPics.length(); i++ ){
					JSONObject pic = myPics.getJSONObject(i);
					newPoint.pics.add(pic.getString("Picture_URL"));
				}

				JSONArray myComments = resPoint.getJSONArray("Comments");
				for (int i = 0 ; i < myPics.length(); i++ ){
					JSONObject comment = myComments.getJSONObject(i);
					newPoint.comments.add(comment.getString("Comment"));
				}
			    
			}

		} catch (Exception e) {

			Log.e("WSPoint", e.getMessage());

		}

		return newPoint;
	}

	public boolean ratePoint(int rate){
		return false;
	}

	public boolean commentPoint(String comment){
		return false;
	}

	public boolean reportComment(String comment_id){
		return false;
	}

}
