package it.tesi.app.dto;

public class RuleResponse {
    private boolean allowed;
    private String action;
    private String message;
    private String resultItem;
    private int resultCount;

    public RuleResponse() {
    }

    public RuleResponse(boolean allowed, String action, String message, String resultItem, int resultCount) {
        this.allowed = allowed;
        this.action = action;
        this.message = "Quarkus: " + message;
        this.resultItem = resultItem;
        this.resultCount = resultCount;
    }

    public boolean isAllowed() {
        return allowed;
    }

    public String getAction() {
        return action;
    }

    public String getMessage() {
        return message;
    }

    public String getResultItem() {
        return resultItem;
    }

    public int getResultCount() {
        return resultCount;
    }
}
