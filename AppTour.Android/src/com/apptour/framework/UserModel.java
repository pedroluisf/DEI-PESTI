package com.apptour.framework;

import java.util.Date;

public class UserModel {

	private static UserModel instance;
	
	public String Id;
    public boolean IsActive;
    public String UserName;
    public String Password;
    public String NewPassword;
    public String Email;
    public String RealName;
    public String Role;
    public Date CreationDate;    
    public Date UpdateDate;

    // Singleton Pattern for User 
    public static UserModel getInstance(){
    	if (instance==null){
    		instance = new UserModel();
    	}
    	return instance;
    }
    
}
