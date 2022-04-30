import { TestService, Data, SpaceService } from "./test-service.service";

describe('TestService', () => {
  
  [
    {
      "data": "World",
      "expected": "Hello~World"
    },
   {
    "data": "Friend",
    "expected": "Hello~Friend"
    },
  ].forEach((dataDrivenTest, index) => {

    it(`[${index}] formatted data should be returned`, () => {
      
      // Arrange
      const spaceServiceMock = jasmine.createSpyObj<SpaceService>(["getSpace"]);
      spaceServiceMock.getSpace.and.returnValue('~');

      const testData: Data = { value: 'Hello' };

      const sut = new TestService(spaceServiceMock, testData);
      
      // Act
      const result = sut.getFormattedData(dataDrivenTest.data);

      // Assert
      expect(result).toBe(dataDrivenTest.expected);
    });
  })
});
