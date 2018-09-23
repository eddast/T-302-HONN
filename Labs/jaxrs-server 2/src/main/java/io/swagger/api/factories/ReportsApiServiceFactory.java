package io.swagger.api.factories;

import io.swagger.api.ReportsApiService;
import io.swagger.api.impl.ReportsApiServiceImpl;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class ReportsApiServiceFactory {
    private final static ReportsApiService service = new ReportsApiServiceImpl();

    public static ReportsApiService getReportsApi() {
        return service;
    }
}
