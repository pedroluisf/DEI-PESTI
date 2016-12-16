package com.apptour.app;

import java.util.ArrayList;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.ImageView;
import android.widget.TextView;

import com.apptour.framework.TopicModel;

public class ThemeAdapter extends BaseExpandableListAdapter {

    private Context context;
    private ArrayList<String> groups;
    private ArrayList<ArrayList<TopicModel>> topics;
    private LayoutInflater inflater;

    public ThemeAdapter(Context context, 
                        ArrayList<String> groups,
						ArrayList<ArrayList<TopicModel>> topics ) { 
        this.context = context;
		this.groups = groups;
        this.topics = topics;
        inflater = LayoutInflater.from( context );
    }

    public Object getChild(int groupPosition, int childPosition) {
        return topics.get( groupPosition ).get( childPosition );
    }

    public long getChildId(int groupPosition, int childPosition) {
        return (long)( groupPosition*1024+childPosition );  // Max 1024 children per group
    }

    public View getChildView(int groupPosition, int childPosition, boolean isLastChild, View convertView, ViewGroup parent) {
        View v = null;
        if( convertView != null )
            v = convertView;
        else
            v = inflater.inflate(R.layout.child_row, parent, false); 
        TopicModel t = (TopicModel)getChild( groupPosition, childPosition );
		TextView tvChild = (TextView)v.findViewById( R.id.tvChild );
		if( tvChild != null )
			tvChild.setText( t.getName() );
		CheckBox cb = (CheckBox)v.findViewById( R.id.chkChild );
        cb.setTag(t.getId());
        
        //Icon
        ImageView img = (ImageView)v.findViewById(R.id.imgChild);
		img.setImageResource(context.getResources().getIdentifier(t.getIconName(),
			    "drawable", context.getPackageName()));
        
    	boolean found = false;
    	for (ArrayList<TopicModel> a : topics){
    		for (TopicModel topics : a){
    			if (topics.getId()==cb.getTag()){
    		        cb.setChecked(topics.isSelected());
    				found = true;
    				break;
    			}
    		}
    		if (found) break;
    	}

        cb.setOnCheckedChangeListener(new OnCheckedChangeListener() {

            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
            	boolean found = false;
            	for (ArrayList<TopicModel> a : topics){
            		for (TopicModel t : a){
            			if (t.getId()==buttonView.getTag()){
            				Log.i("CHECK", "" + buttonView.getTag());
            				t.setSelected(buttonView.isChecked());
            				found = true;
            				break;
            			}
            		}
            		if (found) break;
            	}
            }
          });
        
        return v;
    }

    public int getChildrenCount(int groupPosition) {
        return topics.get( groupPosition ).size();
    }

    public Object getGroup(int groupPosition) {
        return groups.get( groupPosition );        
    }

    public int getGroupCount() {
        return groups.size();
    }

    public long getGroupId(int groupPosition) {
        return (long)( groupPosition*1024 );  // To be consistent with getChildId
    } 

    public View getGroupView(int groupPosition, boolean isExpanded, View convertView, ViewGroup parent) {
        View v = null;
        if( convertView != null )
            v = convertView;
        else
            v = inflater.inflate(R.layout.group_row, parent, false); 
        String gt = (String)getGroup( groupPosition );
		TextView themeGroup = (TextView)v.findViewById( R.id.tvGroup );
		if( gt != null )
			themeGroup.setText( gt );
        return v;
    }

    public boolean hasStableIds() {
        return true;
    }

    public boolean isChildSelectable(int groupPosition, int childPosition) {
        return true;
    } 

    public void onGroupCollapsed (int groupPosition) {} 
    public void onGroupExpanded(int groupPosition) {}

}
