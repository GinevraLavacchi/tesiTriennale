package it.tesi.app;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication //comunico a Spring che questa è la classe principale dell'app
public class Application {
  public static void main(String[] args) {
    SpringApplication.run(Application.class, args);//avvio l'app Spring Boot
  }
}
