package com.apptour.app;

import java.io.InputStream;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.Window;
import android.widget.ImageView;

public class SplashActivity extends Activity {
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        //Remove Title
        requestWindowFeature(Window.FEATURE_NO_TITLE);

        setContentView(R.layout.splash);
        
        //Splash Handler
        Handler x = new Handler();
        x.postDelayed(new SplashHandler(), 2000); // 2000 Milliseconds
    }

    class SplashHandler implements Runnable {
		public void run() {
			startActivity(new Intent(getApplication(),AppTourActivity.class));
			SplashActivity.this.finish();
		}
	}

}
