package it.tesi.app.model;

import it.tesi.app.dto.InventoryItemDto;
import java.util.List;

public class GameContext {
    private String targetType;
    private String recipeName;
    private List<InventoryItemDto> ingredients;

    private boolean allowed = false;
    private String action = "none";
    private String message = "Ricetta non valida";
    private String resultItem = "";
    private int resultCount = 1;

    public String getTargetType() {
        return targetType;
    }

    public void setTargetType(String targetType) {
        this.targetType = targetType;
    }

    public String getRecipeName() {
        return recipeName;
    }

    public void setRecipeName(String recipeName) {
        this.recipeName = recipeName;
    }

    public List<InventoryItemDto> getIngredients() {
        return ingredients;
    }

    public void setIngredients(List<InventoryItemDto> ingredients) {
        this.ingredients = ingredients;
    }

    public boolean isAllowed() {
        return allowed;
    }

    public void setAllowed(boolean allowed) {
        this.allowed = allowed;
    }

    public String getAction() {
        return action;
    }

    public void setAction(String action) {
        this.action = action;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getResultItem() {
        return resultItem;
    }

    public void setResultItem(String resultItem) {
        this.resultItem = resultItem;
    }
    
    public int getResultCount() {
        return resultCount;
    }
    
    public void setResultCount(int resultCount) {
        this.resultCount = resultCount;
    }
}