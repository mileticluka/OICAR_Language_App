package vua.oicar.webfrontend;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.context.annotation.SessionScope;
import vua.oicar.webfrontend.models.LoginSession;

@Configuration
public class Beans {
    @Bean
    @SessionScope
    public LoginSession loginSession() {
        return new LoginSession();
    }
}
