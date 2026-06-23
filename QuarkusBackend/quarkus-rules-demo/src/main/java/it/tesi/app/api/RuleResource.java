package it.tesi.app.api;

import it.tesi.app.dto.RuleRequest;
import it.tesi.app.dto.RuleResponse;
import it.tesi.app.rules.RuleService;
import jakarta.inject.Inject;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;

@Path("/rules")
@Consumes(MediaType.APPLICATION_JSON)
@Produces(MediaType.APPLICATION_JSON)
public class RuleResource {

    @Inject
    RuleService ruleService;

    @POST
    @Path("/evaluate")
    public RuleResponse evaluate(RuleRequest request) {
        return ruleService.evaluate(request);
    }
}