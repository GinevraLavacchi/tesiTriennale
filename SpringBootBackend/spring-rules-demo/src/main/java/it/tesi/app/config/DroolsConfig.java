package it.tesi.app.config;

import org.kie.api.KieServices;
import org.kie.api.runtime.KieContainer;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration //comunico a spring che questa classe def bean/configurazione
public class DroolsConfig {

    @Bean//dice che il metodo restituisce un obj che Spring deve gestire come bean
    public KieContainer kieContainer() {
        // Usa il kmodule.xml sul classpath
        return KieServices.Factory.get().getKieClasspathContainer(); //dico a Drools di cercare il kmodule.xml, le regole nel classpath e di creare il contenitore
    }
}
//KieContainer è il contenitore Drools che carica la config e le regole dal classpath del progetto
//il bean KieContainer è il ponte tra Spring e Drools