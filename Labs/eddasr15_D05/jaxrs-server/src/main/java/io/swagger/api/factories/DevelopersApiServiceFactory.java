package io.swagger.api.factories;

import io.swagger.api.DevelopersApiService;
import io.swagger.api.impl.DevelopersApiServiceImpl;
import is.ru.honn.factory.ServiceFactory;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public class DevelopersApiServiceFactory {
    private final static DevelopersApiService service =
            new DevelopersApiServiceImpl(ServiceFactory.getDeveloperService());

    public static DevelopersApiService getDevelopersApi() {
        return service;
    }
}
