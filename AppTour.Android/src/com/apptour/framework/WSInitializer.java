package com.apptour.framework;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONObject;

import android.util.Log;

public class WSInitializer extends WebService {

	public List<ThemeModel> getAllThemes(String ISO2){
		List<ThemeModel> myList = new ArrayList<ThemeModel>();
		try {

			HashMap<String, String> params = new HashMap<String, String>();
			params.put("language", ISO2);
			String res = performRequest("GetAllThemes", params);

			if (res != null) {
				JSONArray myJsonArray = new JSONArray(res);
				for (int i = 0; i < myJsonArray.length(); i++) {
				    JSONObject row = myJsonArray.getJSONObject(i);
				    ThemeModel newTheme = new ThemeModel();
				    newTheme.setId(row.getString("Id"));
				    newTheme.setName(row.getString("Name"));
				    newTheme.setIconName(row.getString("Image"));
				    myList.add(newTheme);
				}
								
			}

		} catch (Exception e) {

			Log.e("WSThemesPopulator", e.getMessage());

		}
		
		return myList;
	}
	
	public List<TopicModel> getAllTopics(String ISO2){
		List<TopicModel> myTopicsList = new ArrayList<TopicModel>();
		try {

			HashMap<String, String> params = new HashMap<String, String>();
			params.put("language", ISO2);
			String res = performRequest("GetAllTopics", params);

			if (res != null) {
				JSONArray myJsonArray = new JSONArray(res);
				for (int i = 0; i < myJsonArray.length(); i++) {
				    JSONObject row = myJsonArray.getJSONObject(i);

				    TopicModel newTopic = new TopicModel();
				    newTopic.setId(row.getString("Id"));
				    newTopic.setName(row.getString("Name"));
				    newTopic.setIconName(row.getString("Image"));
				    newTopic.setDescription(row.getString("Description"));

				    JSONObject theme = row.getJSONObject("Theme");
				    newTopic.setThemeId(theme.getString("Id"));

				    myTopicsList.add(newTopic);
				}
								
			}

		} catch (Exception e) {

			Log.e("WSTopicsPopulator", e.getMessage());

		}
		
		return myTopicsList;
	}

}
