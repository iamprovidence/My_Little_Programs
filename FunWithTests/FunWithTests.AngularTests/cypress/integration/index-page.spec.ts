import { IndexPage } from "cypress/pages/index-page"

describe('Index page test', () => {
  it('when pressing navigate button test-component should appear', () => {
    // Arrange
    const indexPage = new IndexPage();

    // Act
    indexPage.open();
    indexPage.clickNavigateButton();
    
    // Assert
    indexPage.getTestComponent().contains("test-component works!");
  })
})
