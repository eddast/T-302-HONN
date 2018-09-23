package io.swagger.api.factories;

import io.swagger.api.RatingsApiService;
import io.swagger.api.impl.RatingsApiServiceImpl;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class RatingsApiServiceFactory {
    private final static RatingsApiService service = new RatingsApiServiceImpl();

    public static RatingsApiService getRatingsApi() {
        return service;
    }
}
