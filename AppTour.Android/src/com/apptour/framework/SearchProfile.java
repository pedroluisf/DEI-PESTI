package com.apptour.framework;

import java.util.List;

public class SearchProfile {

	private String searchProfileId;
	private String searchProfileDescription;
	private int distanceSpan;
	private int timeSpan;
	private String searchCriteria;
	private List<String> selectedTopicsId;
	
	public String getSearchProfileId() {
		return searchProfileId;
	}

	public void setSearchProfileId(String searchProfileId) {
		this.searchProfileId = searchProfileId;
	}

	public String getSearchProfileDescription() {
		return searchProfileDescription;
	}

	public void setSearchProfileDescription(String searchProfileDescription) {
		this.searchProfileDescription = searchProfileDescription;
	}

	public int getDistanceSpan() {
		return distanceSpan;
	}

	public void setDistanceSpan(int distanceSpanInMeters) {
		this.distanceSpan = distanceSpanInMeters;
	}

	public int getTimeSpan() {
		return timeSpan;
	}

	public void setTimeSpan(int timeSpan) {
		this.timeSpan = timeSpan;
	}

	public String getSearchCriteria() {
		return searchCriteria;
	}

	public void setSearchCriteria(String searchCriteria) {
		this.searchCriteria = searchCriteria;
	}

	public List<String> getSelectedTopicsId() {
		return selectedTopicsId;
	}

	public void setSelectedTopicsId(List<String> selectedTopicsId) {
		this.selectedTopicsId = selectedTopicsId;
	}

}
