package com.apptour.framework;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import android.widget.Toast;

public abstract class WebService {

	private String HOST_ADDRESS = "http://apptour.dei.isep.ipp.pt/WS/AppTourWS.svc/";

	protected String performRequest(String methodName,
			Map<String, String> params) throws Exception {

		/*
		 * DefaultHttpClient httpClient = new DefaultHttpClient();
		 * 
		 * HttpPost request = new HttpPost(HOST_ADDRESS + methodName);
		 * 
		 * request.setHeader("Accept", "application/json");
		 * request.setHeader("Content-type", "application/json");
		 * 
		 * 
		 * // Fill in Parameters
		 * 
		 * JSONObject inputParams = new JSONObject(); 
		 * for (Map.Entry<String, String> param : params.entrySet()) {
		 * 	inputParams.put((String)param.getKey(), (String)param.getValue()); 
		 * }
		 * 
		 * StringEntity se = new StringEntity(inputParams.toString());
		 * se.setContentType(new BasicHeader(HTTP.CONTENT_TYPE,
		 * "application/json"));
		 * 
		 * request.setEntity(se); HttpResponse response =
		 * httpClient.execute(request); HttpEntity responseEntity =
		 * response.getEntity();
		 * 
		 * if (responseEntity != null) {
		 * 
		 * InputStream instream = responseEntity.getContent(); String result =
		 * convertStreamToString(instream); instream.close(); return result;
		 * 
		 * } else {
		 * 
		 * return null;
		 * 
		 * }
		 */

		HttpClient httpClient = new DefaultHttpClient();
		HttpPost httpPost = new HttpPost(HOST_ADDRESS + methodName);

		List<NameValuePair> nameValuePairList = new ArrayList<NameValuePair>();
		if (params != null){
			for (Map.Entry<String, String> param : params.entrySet()) {
				BasicNameValuePair pair = new BasicNameValuePair((String)param.getKey(), (String)param.getValue());
				nameValuePairList.add(pair);
			}
		}

		try {
			UrlEncodedFormEntity urlEncodedFormEntity = new UrlEncodedFormEntity(nameValuePairList);

			httpPost.setEntity(urlEncodedFormEntity);

			HttpResponse httpResponse = httpClient.execute(httpPost);

			InputStream inputStream = httpResponse.getEntity().getContent();

			InputStreamReader inputStreamReader = new InputStreamReader(inputStream);

			BufferedReader bufferedReader = new BufferedReader(
					inputStreamReader);

			StringBuilder stringBuilder = new StringBuilder();

			String bufferedStrChunk = null;

			while ((bufferedStrChunk = bufferedReader.readLine()) != null) {
				stringBuilder.append(bufferedStrChunk);
			}

			return stringBuilder.toString();

		} catch (UnsupportedEncodingException uee) {
			System.out
					.println("An Exception given because of UrlEncodedFormEntity argument :"
							+ uee);
			uee.printStackTrace();
			return null;
		}

	}

//	public static String convertStreamToString(InputStream is) {
//		BufferedReader reader = new BufferedReader(new InputStreamReader(is));
//		StringBuilder sb = new StringBuilder();
//
//		String line = null;
//		try {
//			while ((line = reader.readLine()) != null) {
//				sb.append(line + "\n");
//			}
//		} catch (IOException e) {
//			e.printStackTrace();
//		} finally {
//			try {
//				is.close();
//			} catch (IOException e) {
//				e.printStackTrace();
//			}
//		}
//		return sb.toString();
//	}

}
