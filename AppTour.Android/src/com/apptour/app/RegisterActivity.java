package com.apptour.app;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.apptour.framework.AuthenticationHelper;

public class RegisterActivity extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.register);
	}

	//Cancel Button
	public void register_cancel(View view) {

	}

	//Confirm Button
	public void register_confirm(View view) {
		Context context = getApplicationContext();
		AuthenticationHelper auth = new AuthenticationHelper(this);
		EditText etUsername = (EditText) findViewById(R.id.etRegisterUsername);
		EditText etPassword = (EditText) findViewById(R.id.etRegisterPassword);
		EditText etRepPassword = (EditText) findViewById(R.id.etRegisterRepPass);
		EditText etEmail = (EditText) findViewById(R.id.etRegisterEmail);
		EditText etRealname = (EditText) findViewById(R.id.etRegisterRealName);

		String username = etUsername.getText().toString();
		String password = etPassword.getText().toString();
		String repPassword = etRepPassword.getText().toString();
		String email = etEmail.getText().toString();
		String realName = etRealname.getText().toString();

		//Validate Fillings
		if (username==null){
			Toast.makeText(context, R.string.username_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (username.trim().equalsIgnoreCase("")){
			Toast.makeText(context, R.string.username_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (password == null){
			Toast.makeText(context, R.string.password_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (password.trim().equalsIgnoreCase("")){
			Toast.makeText(context, R.string.password_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (password.length() < 6){
			Toast.makeText(context, R.string.password_6_chars, Toast.LENGTH_SHORT).show();
			return;
		}
		if (repPassword == null){
			Toast.makeText(context, R.string.rep_password_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (repPassword.trim().equalsIgnoreCase("")){
			Toast.makeText(context, R.string.rep_password_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (!password.equals(repPassword)){
			Toast.makeText(context, R.string.passwords_dont_match, Toast.LENGTH_SHORT).show();
			return;
		}
		if (!password.equals(repPassword)){
			Toast.makeText(context, R.string.passwords_dont_match, Toast.LENGTH_SHORT).show();
			return;
		}
		if (email==null){
			Toast.makeText(context, R.string.email_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (email.trim().equalsIgnoreCase("")){
			Toast.makeText(context, R.string.email_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (realName == null){
			Toast.makeText(context, R.string.real_name_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		if (realName.trim().equalsIgnoreCase("")){
			Toast.makeText(context, R.string.real_name_mandatory, Toast.LENGTH_SHORT).show();
			return;
		}
		
		//Check Username Validity
		if (!auth.usernameValid(etUsername.getText().toString())){
			Toast.makeText(context, R.string.username_already_exists, Toast.LENGTH_SHORT).show();
			return;
		}

		//Check Email Validity
		if (!auth.emailValid(etUsername.getText().toString())){
			Toast.makeText(context, R.string.email_already_exists, Toast.LENGTH_SHORT).show();
			return;
		}
		
		//All clear, let's Register
		if (!auth.register(username, password, email, realName)){
			Toast.makeText(context, R.string.register_failed, Toast.LENGTH_SHORT).show();
			return;
		} else {
			Toast.makeText(context, "ALLELUIA", Toast.LENGTH_SHORT).show();
			return;
		}
		
		
	}
}
