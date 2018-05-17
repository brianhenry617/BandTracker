using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using BandTracker;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandsTests : IDisposable
  {
    public BandsTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=BandTracker;";
    }
    public void Dispose()
    {
      Bands.DeleteAll();
      Venues.DeleteAll();
    }

    [TestMethod]
    public void Equals_OverrideTrueForSameDescription_Book()
    {
      BandTracker bands1 = new Bands("BandOne", "Pop");
      BandTracker bands2 = new Bands("BandTwo", "Latin");

      Assert.AreEqual(bands1, bands2)
    }
    [TestMethod]
    public void Save_SavesItemToDatabase_Bands()
    {
      Bands testBands = new Bands("BandOne", "Pop");
      testBands.Save();

      List<Bands> result = Bands.GetAllBands

      CollectionsAssert.AreEqual(result, testBands);
    }
    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      BandTracker testBands = new Bands("BandOne", "Pop");
      testBands.Save();

      Bands savedBands = Bands.GetAllBands()[0];

      int result = savedBands.GetId();
      int testId = testBands.Save_DatabaseAssignsIdToObject_Id();

      Assert.AreEqual(result, testId);
    }
    [TestMethod]
    public void Find_FindsBandsInDataBase_Bands()
    {
      Bands testBands = new Bands("BandOne", "Pop");
      testBands.Save();

      Bands foundBands = Bands.Find(testBands.GetId());

      Assert.AreEqual(testBands, foundBands);
    }
    [TestMethod]
    public void GetBands_ReturnsAllBands_Venues()
    {
      Bands testBook = new Book("Crime and Punishment", "Novel");
      testBook.Save();

      Author testAuthor1 = new Author("Fyodor Dostoyevsky");
      testAuthor1.Save();

      Author testAuthor2 = new Author("Steven Gerrard");
      testAuthor2.Save();

      testBook.AddAuthor(testAuthor1);
      testBook.AddAuthor(testAuthor2);
      List<Author> result = testBook.GetAuthors();
      List<Author> testList = new List<Author> {testAuthor1, testAuthor2};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Delete_DeletesBookAssociationsFromDatabase_BookList()
    {
      Author testAuthor = new Author("Fyodor Dostoyevsky");
      testAuthor.Save();

      string testBookName = "Mow the lawn";
      string testBookGenre = "Tale";
      Book testBook = new Book(testBookName, testBookGenre);
      testBook.Save();

      testBook.AddAuthor(testAuthor);
      testBook.Delete();

      List<Book> resultAuthorBooks = testAuthor.GetBooks();
      List<Book> testAuthorBooks = new List<Book> {};

      CollectionAssert.AreEqual(testAuthorBooks, resultAuthorBooks);
    }
  }
}
