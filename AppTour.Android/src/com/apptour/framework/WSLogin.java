package com.apptour.framework;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.HashMap;

import org.json.JSONObject;

import android.util.Log;

public class WSLogin extends WebService {

	public UserModel login(String username, String password) {

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
		params.put(new String("username"), username);
		params.put(new String("password"), password);

		try {
			//String res = performRequest("Authentication", params);
			String res = performRequest("Authentication", params);

			if (res != null) {
				JSONObject myJsonUser = new JSONObject(res);

				user.UserName = myJsonUser.getString("UserName");
				user.Email = myJsonUser.getString("Email");
				user.Id = myJsonUser.getString("Id");
				user.IsActive = myJsonUser.getBoolean("IsActive");
				user.RealName = myJsonUser.getString("RealName");
				user.Role = myJsonUser.getString("Role");
				// user.NewPassword = myJsonUser.getString("NewPassword");
				// user.Password = myJsonUser.getString("Password");
				// user.CreationDate =
				// lDateFormatter.parse(myJsonUser.getString("CreationDate"));
				// user.UpdateDate =
				// lDateFormatter.parse(myJsonUser.getString("UpdateDate"));

			}

			return user;

		} catch (Exception e) {

			Log.e("WSLogin", e.getMessage());
			return user;

		}

	}
}
