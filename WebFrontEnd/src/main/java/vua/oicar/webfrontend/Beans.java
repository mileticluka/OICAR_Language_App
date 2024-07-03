package vua.oicar.webfrontend;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import vua.oicar.webfrontend.models.LoginSession;

@Configuration
public class Beans {
    @Bean
    public LoginSession loginSession() {
        return new LoginSession();
    }
}
