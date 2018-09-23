package io.swagger.api.factories;

import io.swagger.api.GamesApiService;
import io.swagger.api.impl.GamesApiServiceImpl;
import is.ru.honn.factory.ServiceFactory;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-13T18:18:20.475Z")
public class GamesApiServiceFactory {
    private final static GamesApiService service =
            new GamesApiServiceImpl(ServiceFactory.getGameService());

    public static GamesApiService getGamesApi() {
        return service;
    }
}
