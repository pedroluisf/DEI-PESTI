package com.apptour.framework;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

public class WSSearchProfile extends WebService {

	public List<SearchProfile> getAllSearchProfiles(String userId) {
		List<SearchProfile> mySearchProfiles = new ArrayList<SearchProfile>();

		HashMap<String, String> params = new HashMap<String, String>();
		params.put("userid", userId);

		try {

			String res = performRequest("SearchProfileSave", params);

			if (res != null) {
				
				JSONArray myJsonArray = new JSONArray(res);
				for (int i = 0; i < myJsonArray.length(); i++) {
				    JSONObject row = myJsonArray.getJSONObject(i);
				    SearchProfile newSP = new SearchProfile();
				    newSP.setSearchProfileId(row.getString("id"));
				    newSP.setSearchProfileDescription(row.getString("description"));
				    newSP.setSearchCriteria(row.getString("criteria"));
				    newSP.setDistanceSpan(row.getInt("distance_span"));
				    newSP.setTimeSpan(row.getInt("time_span"));

				    List<String> myTopics = new ArrayList<String>();
				    JSONArray myJsonTopics = row.getJSONArray("selected_topics");
					for (int j = 0; j < myJsonTopics.length(); j++) {
						JSONObject topic = myJsonArray.getJSONObject(i);
						myTopics.add(topic.getString("topic_id"));
					}
				    newSP.setSelectedTopicsId(myTopics);
				    
				    mySearchProfiles.add(newSP);
				}
								

			}

		} catch (Exception e) {

			Log.e("WSSearchProfile", e.getMessage());

		}

		return mySearchProfiles;

	}

	public SearchProfile saveSearchProfile(SearchProfile mySearchProfile) {

		HashMap<String, String> params = new HashMap<String, String>();
		params.put("id", mySearchProfile.getSearchProfileId());
		params.put("description", mySearchProfile.getSearchProfileDescription());
		params.put("distanceSpan", String.valueOf(mySearchProfile.getDistanceSpan()));
		params.put("TimeSpan", String.valueOf(mySearchProfile.getTimeSpan()));
		params.put("topics", StringUtils.join(mySearchProfile.getSelectedTopicsId(), ";"));

		try {

			String res = performRequest("SearchProfileSave", params);

			if (res != null) {
				
				JSONObject myJsonResponse = new JSONObject(res);
				mySearchProfile.setSearchProfileId(myJsonResponse.getString("id"));

			}

		} catch (Exception e) {

			Log.e("WSSearchProfile", e.getMessage());

		}

		return mySearchProfile;

	}

	public SearchProfile saveNewSearchProfile(SearchProfile mySearchProfile,
			String SearchProfileName) {

		HashMap<String, String> params = new HashMap<String, String>();
		params.put("description", mySearchProfile.getSearchProfileDescription());
		params.put("distanceSpan", String.valueOf(mySearchProfile.getDistanceSpan()));
		params.put("TimeSpan", String.valueOf(mySearchProfile.getTimeSpan()));
		params.put("topics", StringUtils.join(mySearchProfile.getSelectedTopicsId(), ";"));

		try {

			String res = performRequest("SearchProfileSaveAs", params);

			if (res != null) {
				
				JSONObject myJsonResponse = new JSONObject(res);
				mySearchProfile.setSearchProfileId(myJsonResponse.getString("id"));

			}

		} catch (Exception e) {

			Log.e("WSSearchProfile", e.getMessage());

		}

		return mySearchProfile;

	}

	public boolean deleteSearchProfile(SearchProfile mySearchProfile) {

		boolean done = false;
		HashMap<String, String> params = new HashMap<String, String>();
		params.put("id", mySearchProfile.getSearchProfileId());

		try {

			String res = performRequest("SearchProfileDelete", params);

			if (res != null) {

				if (res.replace("\n", "").equalsIgnoreCase("true"))
					done = true;
			}

		} catch (Exception e) {

			Log.e("WSSearchProfile", e.getMessage());

		}

		return done;
	}

}
