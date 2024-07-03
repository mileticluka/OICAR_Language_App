package vua.oicar.webfrontend.controller;


import lombok.AllArgsConstructor;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.client.RestTemplate;
import vua.oicar.webfrontend.models.*;
import vua.oicar.webfrontend.service.ApiService;

import javax.swing.text.html.Option;
import java.util.HashMap;
import java.util.List;
import java.util.Optional;

@Controller
@RequestMapping("/")
@AllArgsConstructor
public class HomeController {
    private final LoginSession session;
    private ApiService apiService;

    @GetMapping
    public String home(Model model) {
        if (session.getToken().isEmpty()) {
            return "redirect:/login";
        }

        Optional<User> user = apiService.getUserInfo(session);

        if (user.isEmpty()) {
            model.addAttribute("message", "No user");
            return "login";
        }

        model.addAttribute("username", user.get().getUsername());
        model.addAttribute("email", user.get().getEmail());

        List<Language> languages = apiService.getLanguages();
        model.addAttribute("languages", languages);

        HashMap<Integer, List<LanguageStat>> languageStats = new HashMap<>();
        for (Language language : languages) {
            List<LanguageStat> stats = apiService.getLanguageStats(session, language.getId());
            languageStats.put(language.getId(), stats);
        }
        model.addAttribute("stats", languageStats);

        return "home";
    }

    @GetMapping("login")
    public String login(Model model) {
        return "login";
    }

    @PostMapping("login")
    public String loginPost(@ModelAttribute LoginRequest request, Model model) {
        Optional<String> token = apiService.login(request);

        if (token.isEmpty()) {
            model.addAttribute("message", "Invalid username or password");
            return "login";
        }

        session.setToken(token);
        session.setUser(apiService.getUserInfo(session));

        return "redirect:/";
    }

    @PostMapping("logout")
    public String logout(Model model) {
        session.setToken(Optional.empty());
        return "redirect:/login";
    }

    @GetMapping("register")
    public String register(Model model) {
        return "register";
    }

    @PostMapping("register")
    public String registerPost(@ModelAttribute RegisterRequest request, Model model) {
        boolean success = apiService.register(request);

        if (!success) {
            model.addAttribute("message", "Invalid input. Please check that everything is correct");
            return "register";
        }

        return "redirect:/login";
    }

    @PostMapping("delete")
    public String delete(Model model) {
        if (!apiService.deleteUser(session)) {
            return "redirect:/";
        }

        session.setToken(Optional.empty());
        model.addAttribute("message", "Your account has been successfully deleted");
        return "redirect:/login";
    }
}
