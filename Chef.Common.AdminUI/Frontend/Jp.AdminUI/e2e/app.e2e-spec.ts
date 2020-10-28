import { ChefWebAppPage } from "./app.po";
import { TestBed } from "@angular/core/testing";
import { HttpClientModule } from "@angular/common/http";

describe("chef WebApp", function () {
  let page: ChefWebAppPage;

  beforeEach(() => {
    page = new ChefWebAppPage();
  });

  it("should display sign-in page", () => {
    page.navigateTo();
    expect(page.getButtonText()).toEqual("Login");
    expect(page.getUrl()).toContain("/login");
  });
});
