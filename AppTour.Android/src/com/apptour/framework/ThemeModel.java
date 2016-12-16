package com.apptour.framework;

import java.util.ArrayList;
import java.util.List;

public class ThemeModel {
	
	private String id;
	private String name;
	private String iconName;
	private boolean selected;
	public List<TopicModel> topics;
	
	public ThemeModel(){
		topics = new ArrayList<TopicModel>();
	}
	
	public String getId() {
		return id;
	}

	public void setId(String themeId) {
		id = themeId;
	}

	public String getName() {
		return name;
	}

	public void setName(String themeName) {
		name = themeName;
	}

	public String getIconName() {
		return iconName;
	}

	public boolean isSelected() {
		return selected;
	}

	public void setSelected(boolean selected) {
		this.selected = selected;
	}

	public void setIconName(String iconName) {
		this.iconName = iconName;
	}

}
