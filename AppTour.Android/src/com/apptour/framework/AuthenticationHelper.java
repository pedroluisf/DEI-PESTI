package com.apptour.framework;

import android.app.Dialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.apptour.app.R;

public class AuthenticationHelper {

	private Context context;
	private ApplicationValues myValues;
	private UserModel user;

	public AuthenticationHelper(Context context) {
		this.context = context;
		myValues = ApplicationValues.getInstance(this.context);
	}

	public Boolean emailValid(String email) {
		WSRegister ws = new WSRegister();
		return ws.isEmailValid(email);
	}

	public Boolean usernameValid(String username) {
		WSRegister ws = new WSRegister();
		return ws.isUsernameValid(username);
	}

	public Boolean register(String username, String Password, String email, String realName) {
		WSRegister ws = new WSRegister();
		user = ws.registerUser(username, Password, email, realName);
		if (user.Id == null){
			Toast toast = Toast.makeText(context,
					R.string.register_failed, Toast.LENGTH_SHORT);
			toast.show();
			return false;
		} else {
			myValues.setUser_id(user.Id);
			myValues.setUsername(username);
			return true;
		}
	}

	public void login() {

		// If we don't have User ID we get data from user...
		if (StringUtils.GUID_EMPTY.equals(myValues.getUser_id())) {
			showLoginDialog();
		}

	}

	private void showLoginDialog() {

		final Dialog dialog = new Dialog(context);

		dialog.setContentView(R.layout.login); 

		dialog.setTitle(R.string.login); 

		final Button ok = (Button) dialog.findViewById(R.id.btn_Login_confirm);
		final Button cancelar = (Button) dialog
				.findViewById(R.id.btn_Login_cancel);

		final EditText etUsername = (EditText) dialog
				.findViewById(R.id.etLoginUsername);
		final EditText etPassword = (EditText) dialog
				.findViewById(R.id.etLoginPassword);

		etUsername.setText(myValues.getUsername());

		ok.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {

				myValues.setUsername(etUsername.getText().toString());

				if (performRemoteLogin(etUsername.getText().toString(), etPassword.getText().toString())) {
					dialog.dismiss();
				} else {
					Toast toast = Toast.makeText(context,
							R.string.login_failed, Toast.LENGTH_SHORT);
					toast.show();
				}

			}

		});

		cancelar.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				dialog.dismiss();
			}
		});

		dialog.show();

	}

	private boolean performRemoteLogin(String username, String password) {

		// Invoke WebService
		WSLogin ws = new WSLogin();
		user = ws.login(username, password);

		if (user.Id != null) {
			
			myValues.setUsername(username);
			myValues.setUser_id(user.Id);

			return true;

		} else {

			return false;
		}

	}

}
