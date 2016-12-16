package com.apptour.framework;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.HashMap;

import org.json.JSONObject;

import android.util.Log;


public class WSRegister extends WebService {

	public Boolean isUsernameValid(String username){

		Boolean valid = false;
		HashMap<String, String> params = new HashMap<String, String>();
		params.put("username", username);

		try {
			String  res = performRequest("UsernameValidation", params);

			if (res != null) {
				if (res.replace("\n", "").equalsIgnoreCase("true")) 
					valid = true;
			}
			
		} catch (Exception e) {
			Log.e("WSUsernameValidation", e.getMessage());
		}

		return valid;

	}

	public Boolean isEmailValid(String email) {

		Boolean valid = false;
		HashMap<String, String> params = new HashMap<String, String>();
		params.put("email", email);

		try {
			String res = performRequest("EmailValidation", params);

			if (res != null) {
				if (res.replace("\n", "").equalsIgnoreCase("true"))
					valid = true;
			}

		} catch (Exception e) {
			Log.e("WSEmailValidation", e.getMessage());
		}

		return valid;

	}

	public UserModel registerUser(String username, String password, String email, String realName) {
		
		DateFormat lDateFormatter = new SimpleDateFormat(
				"yyyy-MM-dd'T'hh:mm:ss");
		UserModel user = UserModel.getInstance();
		user.CreationDate = null;
		user.Email = null;
		user.Id = null;
		user.IsActive = false;
		user.NewPassword = null;
		user.Password = null;
		user.RealName = null;
		user.Role = null;
		user.UpdateDate = null;
		user.UserName = null;

		HashMap<String, String> params = new HashMap<String, String>();
		params.put("username", username);
		params.put("password", password);
		params.put("email", email);
		params.put("realname", realName);

		try {

			String  res = performRequest("Registration", params);

			if (res != null) {
				JSONObject myJsonUser = new JSONObject(res);

				user.UserName = myJsonUser.getString("UserName");
				user.Email = myJsonUser.getString("Email");
				user.Id = myJsonUser.getString("Id");
				user.IsActive = myJsonUser.getBoolean("IsActive");
				user.RealName = myJsonUser.getString("RealName");
				user.Role = myJsonUser.getString("Role");
//				user.NewPassword = myJsonUser.getString("NewPassword");
//				user.Password = myJsonUser.getString("Password");
//				user.CreationDate = lDateFormatter.parse(myJsonUser.getString("CreationDate"));
//				user.UpdateDate = lDateFormatter.parse(myJsonUser.getString("UpdateDate"));

			}

			return user;

		} catch (Exception e) {

			Log.e("WSRegister", e.getMessage());
			return user;

		}

	}

}
