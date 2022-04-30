export class IndexPage {
    public open(): void{
        cy.visit('/');
    }

    public clickNavigateButton(): void{
        cy.get("button").click();
    }
    
    public getTestComponent(): Cypress.Chainable<JQuery<HTMLElement>>{
        return cy.get("app-test-component");
    }
}