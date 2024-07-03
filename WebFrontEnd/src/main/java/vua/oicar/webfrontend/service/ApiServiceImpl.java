package vua.oicar.webfrontend.service;


import lombok.AllArgsConstructor;
import org.json.JSONArray;
import org.json.JSONObject;
import org.springframework.http.*;
import org.springframework.stereotype.Service;
import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.client.RestTemplate;
import vua.oicar.webfrontend.models.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
@AllArgsConstructor
public class ApiServiceImpl implements ApiService {
    private static final String URL = "http://138.68.77.85:5000/api/";

    @Override
    public Optional<String> login(LoginRequest request) {
        try {
            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_JSON);

            HttpEntity<LoginRequest> entity = new HttpEntity<>(request);
            RestTemplate restTemplate = new RestTemplate();
            ResponseEntity<String> response = restTemplate.exchange(URL + "Auth/login", HttpMethod.POST, entity, String.class);

            if (response.getStatusCode().is4xxClientError()) {
                return Optional.empty();
            }

            String body = response.getBody();
            JSONObject obj = new JSONObject(body);
            return Optional.of(obj.getString("token"));
        } catch (HttpClientErrorException e) {
            return Optional.empty();
        }
    }

    @Override
    public boolean register(RegisterRequest request) {
        try {
            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_JSON);

            HttpEntity<RegisterRequest> entity = new HttpEntity<>(request);
            RestTemplate restTemplate = new RestTemplate();
            ResponseEntity<String> response = restTemplate.exchange(URL + "Auth/register", HttpMethod.POST, entity, String.class);

            return response.getStatusCode().is2xxSuccessful();
        } catch (HttpClientErrorException e) {
            e.printStackTrace();
            return false;
        }
    }

    @Override
    public Optional<User> getUserInfo(LoginSession session) {
        try {
            if (session.getToken().isEmpty()) throw new IllegalStateException("No token");

            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_JSON);
            headers.add("Authorization" , "Bearer " + session.getToken().get());

            RestTemplate restTemplate = new RestTemplate();
            HttpEntity<String> entity = new HttpEntity<>(headers);
            ResponseEntity<String> response = restTemplate.exchange(URL + "User", HttpMethod.GET, entity, String.class);

            if (response.getStatusCode().is4xxClientError()) {
                return Optional.empty();
            }

            String body = response.getBody();
            JSONObject obj = new JSONObject(body);
            String username = obj.getString("username");
            String email = obj.getString("email");

            return Optional.of(new User(username, email));
        } catch (HttpClientErrorException e) {
            e.printStackTrace();
            return Optional.empty();
        }
    }

    @Override
    public List<Language> getLanguages() {
        try {
            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_JSON);

            HttpEntity<LoginRequest> entity = new HttpEntity<>(headers);
            RestTemplate restTemplate = new RestTemplate();
            ResponseEntity<String> response = restTemplate.exchange(URL + "Lang", HttpMethod.GET, entity, String.class);

            if (response.getStatusCode().is4xxClientError()) {
                return List.of();
            }

            String body = response.getBody();
            JSONArray array = new JSONArray(body);

            List<Language> languages = new ArrayList<>();
            for (int i = 0; i < array.length(); i++) {
                int id = array.getJSONObject(i).getInt("id");
                String name = array.getJSONObject(i).getString("name");
                String iconPath = array.getJSONObject(i).getString("iconPath");

                languages.add(new Language(id, name, iconPath));
            }

            return languages;
        } catch (HttpClientErrorException e) {
            e.printStackTrace();
            return List.of();
        }
    }

    @Override
    public List<LanguageStat> getLanguageStats(LoginSession session, int id) {
        try {
            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_JSON);
            headers.add("Authorization" , "Bearer " + session.getToken().get());

            HttpEntity<LoginRequest> entity = new HttpEntity<>(headers);
            RestTemplate restTemplate = new RestTemplate();
            ResponseEntity<String> response = restTemplate.exchange(URL + "stats/" + id, HttpMethod.GET, entity, String.class);

            if (response.getStatusCode().is4xxClientError()) {
                return List.of();
            }

            String body = response.getBody();
            JSONArray array = new JSONArray(body);

            List<LanguageStat> stats = new ArrayList<>();
            for (int i = 0; i < array.length(); i++) {
                String name = array.getJSONObject(i).getString("statName");
                int score = array.getJSONObject(i).getInt("score");

                stats.add(new LanguageStat(name, score));
            }

            return stats;
        } catch (HttpClientErrorException e) {
            e.printStackTrace();
            return List.of();
        }
    }

    @Override
    public boolean deleteUser(LoginSession session) {
        try {
            if (session.getToken().isEmpty()) throw new IllegalStateException("No token");

            HttpHeaders headers = new HttpHeaders();
            headers.setContentType(MediaType.APPLICATION_JSON);
            headers.add("Authorization" , "Bearer " + session.getToken().get());

            RestTemplate restTemplate = new RestTemplate();
            HttpEntity<String> entity = new HttpEntity<>(headers);
            ResponseEntity<String> response = restTemplate.exchange(URL + "user/delete", HttpMethod.DELETE, entity, String.class);

            return response.getStatusCode().is2xxSuccessful();
        } catch (HttpClientErrorException e) {
            e.printStackTrace();
            return false;
        }
    }

    @Override
    public boolean isValidSession(LoginSession session) {
        return true;
    }
}
