package it.tesi.app.rules;

import it.tesi.app.dto.InventoryItemDto;
import it.tesi.app.dto.RuleRequest;
import it.tesi.app.dto.RuleResponse;
import it.tesi.app.model.GameContext;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import org.kie.api.runtime.KieContainer;
import org.kie.api.runtime.KieSession;

@ApplicationScoped
public class RuleService {

    @Inject
    KieContainer kieContainer;

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

            int fired = ksession.fireAllRules();

            System.out.println("Regole attivate: " + fired);
            System.out.println("Allowed finale: " + context.isAllowed());
            System.out.println("Action finale: " + context.getAction());
            System.out.println("ResultItem finale: " + context.getResultItem());
            System.out.println("ResultCount finale: " + context.getResultCount());

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