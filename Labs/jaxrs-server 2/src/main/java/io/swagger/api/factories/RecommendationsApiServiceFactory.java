package io.swagger.api.factories;

import io.swagger.api.RecommendationsApiService;
import io.swagger.api.impl.RecommendationsApiServiceImpl;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class RecommendationsApiServiceFactory {
    private final static RecommendationsApiService service = new RecommendationsApiServiceImpl();

    public static RecommendationsApiService getRecommendationsApi() {
        return service;
    }
}
