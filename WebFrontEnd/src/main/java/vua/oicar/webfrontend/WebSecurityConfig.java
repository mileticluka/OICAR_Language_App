//package vua.oicar.webfrontend;
//
//import lombok.AllArgsConstructor;
//import org.springframework.context.annotation.Bean;
//import org.springframework.context.annotation.Configuration;
//import org.springframework.security.config.annotation.web.builders.HttpSecurity;
//import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
//import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
//import org.springframework.security.config.annotation.web.configurers.LogoutConfigurer;
//import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
//import org.springframework.security.web.SecurityFilterChain;
//import org.springframework.security.web.csrf.CookieCsrfTokenRepository;
//import org.springframework.web.filter.RequestContextFilter;
//import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;
//import vua.oicar.webfrontend.models.LoginSession;
//
//@Configuration
//@EnableWebSecurity
//@AllArgsConstructor
//public class WebSecurityConfig implements WebMvcConfigurer {
//    @Bean
//    public SecurityFilterChain securityFilterChain(HttpSecurity http, RequestContextFilter requestContextFilter) throws Exception {
//        http
//                .authorizeHttpRequests((requests) -> requests
//                        .requestMatchers("/css/*.css").permitAll()
//                        .requestMatchers("/js/*.js").permitAll()
//                        .requestMatchers("/img/*").permitAll()
//
//                        .requestMatchers("/").permitAll()
//                        .requestMatchers("/**").permitAll()
//
//                        .anyRequest().authenticated()
//                )
//                .formLogin((form) -> form
//                        .defaultSuccessUrl("/", true)
//                )
//                .logout(LogoutConfigurer::permitAll);
//
//        // for testing
//        http.csrf(AbstractHttpConfigurer::disable);
//        http.cors(AbstractHttpConfigurer::disable);
//
//        http.csrf(config -> config.csrfTokenRepository(CookieCsrfTokenRepository.withHttpOnlyFalse()));
//
//        return http.build();
//    }
//
//    @Bean
//    public BCryptPasswordEncoder passwordEncoder() {
//        return new BCryptPasswordEncoder();
//    }
//
//    @Bean
//    public LoginSession loginSession() {
//        return new LoginSession();
//    }
//}
