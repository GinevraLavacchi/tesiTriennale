package it.tesi.app.api;

import it.tesi.app.dto.RuleRequest;
import it.tesi.app.dto.RuleResponse;
import it.tesi.app.rules.RuleService;
import org.springframework.web.bind.annotation.*;

@RestController //comunico a Spring che la classe riceve richieste HTTP
@RequestMapping("/rules") //prefisso comune per tutti gli endpoint della classe
@CrossOrigin(origins = "*") //permette chiamate da origini diverse, evita prob di CORS quando il client non è servito dallo stesso host
public class RuleController {

    private final RuleService ruleService;

    public RuleController(RuleService ruleService) {
        this.ruleService = ruleService;
    }

    @PostMapping("/evaluate") //il bean creato viene iniettato qui
    public RuleResponse evaluate(@RequestBody RuleRequest request) {//@RequestBody prende il JSON della richiesta e lo converte in un oggetto RuleRequest
        return ruleService.evaluate(request);//delega la logica al service
    }
}