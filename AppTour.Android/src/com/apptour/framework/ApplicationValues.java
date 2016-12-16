package com.apptour.framework;

import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;

import com.google.android.maps.GeoPoint;


public class ApplicationValues extends Application{
	
	private static ApplicationValues instance;

	private Context context;
	private SharedPreferences pref;
	private String user_id;
	private String username;
	private String ISO2;
	private String SearchProfileId;
	private GeoPoint currentLocation;
	
	private static List<PointModel> points; // Points 2 Show 
	private static List<ThemeModel> themes; // Themes Template
	private static List<SearchProfile> searchProfiles; // Searchprofiles

	// Singleton Pattern 
    public static ApplicationValues getInstance(Context context){
    	if (instance==null){
    		instance = new ApplicationValues(context);
    	}
    	return instance;
    }

	private ApplicationValues(Context c){
		// Have we got Data to Load?
		this.context = c;
		try {

			pref = context.getSharedPreferences("Authentication",
					Context.MODE_PRIVATE);
			user_id = pref.getString("user_id", StringUtils.GUID_EMPTY);
			username = pref.getString("username", null);

			pref = context.getSharedPreferences("Preferences",
					Context.MODE_PRIVATE);
			ISO2 = pref.getString("ISO2", null);
			SearchProfileId = pref.getString("SearchProfileId", StringUtils.GUID_EMPTY);

		} catch (Exception e) {
		}

		//Get the phone's language
		ISO2 = Locale.getDefault().getLanguage();
		if (ISO2 == null){
			ISO2 = "IN";
		}
		
		//Set default value as Rotunda da Boavista
		currentLocation = new GeoPoint((int) (41.15794 * 1E6), (int) (-8.6291 * 1E6));

		//Pure initialization
		points = new ArrayList<PointModel>();
		themes = new ArrayList<ThemeModel>();

	}
	
	public String getUser_id() {
		return user_id;
	}

	public void setUser_id(String user_id) {
		this.user_id = user_id;
		pref = context.getSharedPreferences(
				"Authentication", Context.MODE_PRIVATE);
		pref.edit().putString("user_id", user_id).commit();
	}

	public String getUsername() {
		return username;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public String getISO2() {
		return ISO2;
	}

	public String getSearchProfileId() {
		return SearchProfileId;
	}

	public void setSearchProfileId(String searchProfileId) {
		SearchProfileId = searchProfileId;
		pref = context.getSharedPreferences(
				"Preferences", Context.MODE_PRIVATE);
		pref.edit().putString("SearchProfileId", SearchProfileId).commit();
	}

	public void setCurrentLocation(int latitude, int longitude){
		//myLocation = new GeoPoint((int) (latitude * 1E6), (int) (longitude * 1E6)); //If we received doubles....
		currentLocation = new GeoPoint(latitude, longitude);
	}
	
	public void setCurrentLocation(GeoPoint point){
		currentLocation = point;
	}
	
	public GeoPoint getCurrentLocation(){
		return currentLocation;
	}
	
	@SuppressWarnings("static-access")
	public void setPoints(List<PointModel> points){
    	this.points = points;
    }
    
    public List<PointModel> getPoints(){
    	return points;
    }

	public static List<ThemeModel> getThemes() {
		return themes;
	}

	public static void setThemes(List<ThemeModel> themes) {
		ApplicationValues.themes = themes;
	}

	public static List<SearchProfile> getSearchProfiles() {
		return searchProfiles;
	}

	public static void setSearchProfiles(List<SearchProfile> searchProfiles) {
		ApplicationValues.searchProfiles = searchProfiles;
	}

}
