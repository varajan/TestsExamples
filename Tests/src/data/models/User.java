package data.models;

import data.Constants;

public class User {
    private String login;
    private String email;
    private String password;
    private String password2;

    public User(String login){
        this(login, Constants.Password);
    }

    public User(String login, String password){
        this.login = login;
        this.email = login + "@test.com";
        this.password = password;
        this.password2 = password;
    }

    public String getLogin(){ return login; }
    public String getEmail(){ return email; }
    public String getPassword(){ return password; }
    public String getPassword2(){ return password2; }
}
