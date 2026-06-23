package it.tesi.app.rules;

import it.tesi.app.dto.InventoryItemDto;
import it.tesi.app.dto.RuleRequest;
import it.tesi.app.dto.RuleResponse;
import it.tesi.app.model.GameContext;
import org.kie.api.runtime.KieContainer;
import org.kie.api.runtime.KieSession;
import org.springframework.stereotype.Service;

@Service //dico a Spring che questa classe è un service quindi un bean applicativo gestito dal container
public class RuleService {

    private final KieContainer kieContainer; //qui c'è il contenuto principale Drools

    public RuleService(KieContainer kieContainer) {
        this.kieContainer = kieContainer;//spring inietta il KieContainer
    }

    public RuleResponse evaluate(RuleRequest request) {
        GameContext context = new GameContext();
    
        context.setTargetType(request.getTargetType());
        context.setRecipeName(request.getRecipeName());
        context.setIngredients(request.getIngredients());
    
        KieSession ksession = kieContainer.newKieSession("rulesSession");
    
        try {
            ksession.insert(context);
    
            if (context.getIngredients() != null) {
                for (InventoryItemDto item : context.getIngredients()) {
                    ksession.insert(item);
                }
            }
    
            ksession.fireAllRules();
    
        } finally {
            ksession.dispose();
        }
    
        return new RuleResponse(
            context.isAllowed(),
            context.getAction(),
            context.getMessage(),
            context.getResultItem(),
            context.getResultCount()
        );
    }
}