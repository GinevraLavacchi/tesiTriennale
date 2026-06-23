package it.tesi.app.dto;

import java.util.List;

public class RuleRequest {
    private String targetType;
    private String recipeName;
    private List<InventoryItemDto> ingredients;

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
}