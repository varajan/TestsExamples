package test.java.data;

import java.io.IOException;
import java.security.KeyManagementException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;

import org.apache.hc.client5.http.classic.methods.HttpDelete;
import org.apache.hc.client5.http.classic.methods.HttpPost;
import org.apache.hc.client5.http.impl.classic.CloseableHttpClient;
import org.apache.hc.client5.http.impl.classic.HttpClients;
import org.apache.hc.client5.http.impl.io.PoolingHttpClientConnectionManager;
import org.apache.hc.client5.http.socket.ConnectionSocketFactory;
import org.apache.hc.client5.http.socket.PlainConnectionSocketFactory;
import org.apache.hc.client5.http.ssl.NoopHostnameVerifier;
import org.apache.hc.client5.http.ssl.SSLConnectionSocketFactory;
import org.apache.hc.client5.http.ssl.TrustSelfSignedStrategy;
import org.apache.hc.core5.http.ContentType;
import org.apache.hc.core5.http.HttpEntity;
import org.apache.hc.core5.http.config.Registry;
import org.apache.hc.core5.http.config.RegistryBuilder;
import org.apache.hc.core5.http.io.entity.StringEntity;
import org.apache.hc.core5.ssl.SSLContexts;

import org.codehaus.jackson.map.ObjectMapper;
import org.codehaus.jackson.map.ObjectWriter;
import test.java.data.models.User;

import javax.net.ssl.SSLContext;

public class Users {
    private static CloseableHttpClient newClient() throws KeyManagementException, NoSuchAlgorithmException, KeyStoreException {
        SSLContext context = SSLContexts.custom()
                .loadTrustMaterial(TrustSelfSignedStrategy.INSTANCE)
                .build();

        Registry<ConnectionSocketFactory> registry = RegistryBuilder.<ConnectionSocketFactory> create()
                .register("http", PlainConnectionSocketFactory.INSTANCE)
                .register("https", new SSLConnectionSocketFactory(context, NoopHostnameVerifier.INSTANCE))
                .build();

        PoolingHttpClientConnectionManager connectionManager = new PoolingHttpClientConnectionManager(registry);

        return HttpClients.custom()
                .setConnectionManager(connectionManager)
                .build();
    }

	public static void deleteAll() throws IOException, NoSuchAlgorithmException, KeyStoreException, KeyManagementException {
        HttpDelete delete = new HttpDelete(Constants.BaseUrl + "api/register/deleteAll");
        newClient().execute(delete);
	}
	
	public static void delete(String name) throws IOException, NoSuchAlgorithmException, KeyStoreException, KeyManagementException {
		HttpDelete delete = new HttpDelete(Constants.BaseUrl + "api/register/delete");
        delete.setEntity(getContent(name));

        newClient().execute(delete);
	}

    public static void register(String name) throws NoSuchAlgorithmException, KeyStoreException, KeyManagementException, IOException {
        HttpPost post = new HttpPost(Constants.BaseUrl + "api/register");
        post.setEntity(getContent(name));

        newClient().execute(post);
    }

    private static HttpEntity getContent(String name) throws IOException {
        ObjectWriter ow = new ObjectMapper().writer().withDefaultPrettyPrinter();
        String json = ow.writeValueAsString(new User(name));

        return new StringEntity(json, ContentType.APPLICATION_JSON);
    }
}
