package com.apptour.app;

import java.util.ArrayList;
import java.util.List;

import android.app.ExpandableListActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.CheckBox;
import android.widget.ExpandableListView;
import android.widget.Toast;

import com.apptour.framework.ApplicationValues;
import com.apptour.framework.ThemeModel;
import com.apptour.framework.TopicModel;

public class AppTourSearchThemes extends ExpandableListActivity {

	private ApplicationValues myValues;
	private List<ThemeModel> myThemes;	
    private ThemeAdapter expListAdapter;
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle icicle)
    {
        super.onCreate(icicle);
        setContentView(R.layout.searchthemes);
		myValues = ApplicationValues.getInstance(this);
		myThemes = myValues.getThemes();

		ArrayList<String> groupNames = new ArrayList<String>();
        for (ThemeModel t : myThemes) {
        	groupNames.add( t.getName() );	
        }
        
        ArrayList<ArrayList<TopicModel>> topics = new ArrayList<ArrayList<TopicModel>>(); 
        for (ThemeModel t : myThemes) {
            ArrayList<TopicModel> topic = new ArrayList<TopicModel>();
            for (TopicModel to : t.topics){
                topic.add(to);        	
            }
            topics.add(topic);
		}
		expListAdapter = new ThemeAdapter( this,groupNames, topics );
		setListAdapter( expListAdapter );
	}

    public void onContentChanged  () {
        super.onContentChanged();
        //Do Something
    }

    public boolean onChildClick(
            ExpandableListView parent, 
            View v, 
            int groupPosition,
            int childPosition,
            long id) {

		Toast.makeText(AppTourSearchThemes.this,
				"Row ID " + id, Toast.LENGTH_SHORT)
				.show();
		
        CheckBox cb = (CheckBox)v.findViewById( R.id.chkChild );
        if( cb != null )
            cb.toggle();

        return false;
        
    }
}
