package test.java.utilities;

public class Result {
	private boolean isSuccessful;
	private String message;
	
	public Result(boolean isSuccessful, String message) {
		this.isSuccessful = isSuccessful;
		this.message = message;
	}
	
	public boolean isSuccessful() {
		return this.isSuccessful;
	}
	
	public String getMessage() {
		return this.message;
	}
}