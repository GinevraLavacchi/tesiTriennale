package it.tesi.app.config;

import jakarta.enterprise.context.ApplicationScoped;
import jakarta.enterprise.inject.Produces;
import org.kie.api.KieServices;
import org.kie.api.runtime.KieContainer;

@ApplicationScoped
public class DroolsProducer {

    @Produces
    @ApplicationScoped
    public KieContainer kieContainer() {
        return KieServices.Factory.get().getKieClasspathContainer();
    }
}