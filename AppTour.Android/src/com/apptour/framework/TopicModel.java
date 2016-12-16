package com.apptour.framework;

public class TopicModel {

	private String ThemeId;
	private String id;
	private String name;
	private String description;
	private String iconName;
	private boolean selected;

	public String getThemeId() {
		return ThemeId;
	}
	public void setThemeId(String themeId) {
		ThemeId = themeId;
	}
	public String getId() {
		return id;
	}
	public void setId(String topicId) {
		id = topicId;
	}
	public String getName() {
		return name;
	}
	public void setName(String topicName) {
		name = topicName;
	}
	public String getDescription() {
		return description;
	}
	public void setDescription(String topicDescription) {
		description = topicDescription;
	}
	public String getIconName() {
		return iconName;
	}
	public void setIconName(String iconName) {
		this.iconName = iconName;
	}
	public boolean isSelected() {
		return selected;
	}
	public void setSelected(boolean selected) {
		this.selected = selected;
	}
	
}
