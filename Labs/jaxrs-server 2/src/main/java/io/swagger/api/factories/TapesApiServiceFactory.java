package io.swagger.api.factories;

import io.swagger.api.TapesApiService;
import io.swagger.api.impl.TapesApiServiceImpl;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class TapesApiServiceFactory {
    private final static TapesApiService service = new TapesApiServiceImpl();

    public static TapesApiService getTapesApi() {
        return service;
    }
}
