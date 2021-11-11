package utilities;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.apache.hc.client5.http.classic.methods.HttpDelete;
import org.apache.hc.client5.http.classic.methods.HttpPost;
import org.apache.hc.client5.http.entity.UrlEncodedFormEntity;
import org.apache.hc.client5.http.impl.classic.CloseableHttpClient;
import org.apache.hc.client5.http.impl.classic.HttpClients;
import org.apache.hc.core5.http.NameValuePair;
import org.apache.hc.core5.http.message.BasicNameValuePair;

public class Users {
	public static void deleteAll() throws IOException {
		CloseableHttpClient httpClient = HttpClients.createDefault();
        HttpDelete delete = new HttpDelete(Constants.BaseUrl + "/Register/DeleteAll");
        httpClient.execute(delete);
	}
	
	public static void delete(String name) throws IOException {
		CloseableHttpClient httpClient = HttpClients.createDefault();
		HttpDelete delete = new HttpDelete(Constants.BaseUrl + "/Register/Delete");

        List<NameValuePair> params = new ArrayList<>();
        params.add(new BasicNameValuePair("login", name));

        delete.setEntity(new UrlEncodedFormEntity(params));
        httpClient.execute(delete);
	}
	
	public static void register(String name) throws IOException {
		CloseableHttpClient httpClient = HttpClients.createDefault();
        HttpPost post = new HttpPost(Constants.BaseUrl + "/Register/Register");

        List<NameValuePair> params = new ArrayList<>();
        params.add(new BasicNameValuePair("login", name));
        params.add(new BasicNameValuePair("email", name + "@test.com"));
        params.add(new BasicNameValuePair("password", Constants.Password));

        post.setEntity(new UrlEncodedFormEntity(params));
        httpClient.execute(post);
    }
}
