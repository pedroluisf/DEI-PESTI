package com.apptour.app;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import android.app.ExpandableListActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.CheckBox;
import android.widget.ExpandableListView;
import android.widget.SimpleExpandableListAdapter;
import android.widget.Toast;

import com.apptour.framework.ApplicationValues;
import com.apptour.framework.ThemeModel;
import com.apptour.framework.TopicModel;

public class AppTourSearchThemes_old extends ExpandableListActivity {

	private ApplicationValues myValues;
	private List<ThemeModel> myThemes;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.searchthemes);
		myValues = ApplicationValues.getInstance(this);
		myThemes = myValues.getThemes();
		buildMyExpandableList();
	}

	private void buildMyExpandableList(){
		//Expandable List (Themes / Topics)
		SimpleExpandableListAdapter expListAdapter = new SimpleExpandableListAdapter(
				this, createGroupList(), // groupData describes the first-level entries
				R.layout.group_row, // Layout for the first-level entries
				new String[] { "theme" }, // Key in the groupData maps to display
				new int[] { R.id.tvGroup }, // Data under "colorName" key goes into this TextView
				createChildList(), // childData describes second-level entries
				R.layout.child_row, // Layout for second-level entries
				new String[] { "topic" }, // Keys in childData maps to display
				new int[] { R.id.tvChild } // Data under the keys above go into these TextViews
		);

		setListAdapter(expListAdapter);
		
		// To get Image use this
		//getResources().getIdentifier("ic_launcher", "drawable", "com.apptour.app")
					
	}

    public void onContentChanged  () {
        super.onContentChanged();
		Toast.makeText(AppTourSearchThemes_old.this,
				"Content Changed ", Toast.LENGTH_SHORT)
				.show();
		
    }

	public boolean onChildClick(ExpandableListView parent, 
            View v, 
            int groupPosition,
            int childPosition,
            long id) {
		
		Toast.makeText(AppTourSearchThemes_old.this,
				"Row ID " + id, Toast.LENGTH_SHORT)
				.show();
		
//		CheckBox cb = (CheckBox)v.findViewById( R.id.chkChild );
//        if( cb != null )
//            cb.toggle();
        
        return false;
    }
	
	private List createGroupList() {
		ArrayList result = new ArrayList();
		for (int i = 0; i < myThemes.size(); ++i) {
			HashMap m = new HashMap();
			m.put("theme", myThemes.get(i).getName());
			result.add(m);
		}
		return (List) result;
	}

	private List createChildList() {
		List<TopicModel> topics;
		ArrayList result = new ArrayList();
		for (int i = 0; i < myThemes.size(); ++i) {
			// Second-level lists
			ArrayList secList = new ArrayList();
			int size = myThemes.get(i).topics.size();
			List<TopicModel> myTopics = myThemes.get(i).topics;
			for (int n = 0; n < size; n ++) {
				HashMap child = new HashMap();
				child.put("topic", myTopics.get(n).getName());
				secList.add(child);
			}
			result.add(secList);
		}
		return result;
	}

}
