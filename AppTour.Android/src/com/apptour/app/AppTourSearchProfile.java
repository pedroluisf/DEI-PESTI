package com.apptour.app;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.SeekBar;
import android.widget.SeekBar.OnSeekBarChangeListener;
import android.widget.Spinner;
import android.widget.TextView;

import com.apptour.framework.ApplicationValues;
import com.apptour.framework.SearchProfile;
import com.apptour.framework.WSSearchProfile;

public class AppTourSearchProfile extends Activity {

	private ApplicationValues myValues;
	private SeekBar sbDistance, sbTimespan; 
    private TextView tvDistance, tvTimespan;
    private Spinner spSearchProfile;
    
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.searchprofile);
		myValues = ApplicationValues.getInstance(this);

		//Call Web Service to get all search Profiles
		spSearchProfile = (Spinner)findViewById(R.id.spSearchProfile);
		WSSearchProfile ws = new WSSearchProfile();
		List<SearchProfile> myList = ws.getAllSearchProfiles(myValues.getISO2());

		List<String> list = new ArrayList<String>();
		for (SearchProfile s : myList) {
			list.add(s.getSearchProfileDescription());
		}
		ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this,
			android.R.layout.simple_spinner_item, list);
		dataAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spSearchProfile.setAdapter(dataAdapter);
		
		//Spinner Listener
		
		
		//SeekBar Listeners (Distance)
		tvDistance = (TextView)findViewById(R.id.tvSearchDistance);
		sbDistance = (SeekBar)findViewById(R.id.sbSearchDistance);
        sbDistance.setOnSeekBarChangeListener(new OnSeekBarChangeListener(){   
        	public void onStopTrackingTouch(SeekBar seekBar) {
        		//Do Nothing
        	}
        	public void onStartTrackingTouch(SeekBar seekBar){
	    		//Do Nothing
	    	}
        	public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser){
        		tvDistance.setText("" + progress + " Km");
        	}
        }); 
        
		//SeekBar Listeners (TimeSpan)
		tvTimespan = (TextView)findViewById(R.id.tvSearchTimeSpan);
		sbTimespan = (SeekBar)findViewById(R.id.sbSearchTimespan);
		sbTimespan.setOnSeekBarChangeListener(new OnSeekBarChangeListener(){   
        	public void onStopTrackingTouch(SeekBar seekBar) {
        		//Do Nothing
        	}
        	public void onStartTrackingTouch(SeekBar seekBar){
	    		//Do Nothing
	    	}
        	public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser){
        		tvTimespan.setText(progress + " " + getString(R.string.days));
        	}
        }); 
     
	}
	
	public void setThemes(View view){
		startActivity(new Intent(getApplication(),AppTourSearchThemes.class));
	}

	public void save(View view){
		
	}
	
	public void saveAs(View view){
		
	}

	public void delete(View view){
		
	}
}
