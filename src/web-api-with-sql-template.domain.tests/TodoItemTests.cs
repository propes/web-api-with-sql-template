using System;
using FluentAssertions;
using WebApiWithSqlTemplate.Domain.Models;
using Xunit;

namespace WebApiWithSqlTemplate.Domain.Tests
{
    public class TodoItemTests
    {
        [Fact]
        public void ConstructionShouldSetId()
        {
            var todoItem = new TodoItem();

            todoItem.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void ConstructionShouldSetDateCreated()
        {
            var todoItem = new TodoItem();

            todoItem.DateCreated.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void ConstructionShouldSetDateModified()
        {
            var todoItem = new TodoItem();

            todoItem.DateModified.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void ConstructionShouldInitialiseState()
        {
            var todoItem = new TodoItem();

            todoItem.IsComplete.Should().BeFalse();
            todoItem.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void ConstructionShouldSetDescriptionWhenProvided()
        {
            var todoItem = new TodoItem("Walk the dog");

            todoItem.Description.Should().Be("Walk the dog");
        }

        [Fact]
        public void UpdateDescriptionShouldUpdateState()
        {
            var todoItem = new TodoItem();
            var input = new String('i', 150);
            
            todoItem.UpdateDescription(input);

            todoItem.Description.Should().Be(input);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void UpdateDescriptionShouldThrowWhenNullEmpytyOrWhitespace(string input)
        {
            var todoItem = new TodoItem();

            todoItem
                .Invoking(t => t.UpdateDescription(input))
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void UpdateDescriptionShouldThrowWhenLongerThanMaxCharacterLength()
        {
            var todoItem = new TodoItem();
            var input = new String('i', 151);
            
            todoItem
                .Invoking(t => t.UpdateDescription(input))
                .Should()
                .Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(" Walk the dog")]
        [InlineData("Walk the dog ")]
        [InlineData(" Walk the dog ")]
        public void UpdateDescriptionShouldTrim(string input)
        {
            var todoItem = new TodoItem();

            todoItem.UpdateDescription(input);

            todoItem.Description.Should().Be("Walk the dog");
        }

        [Fact]
        public void UpdateDescriptionShouldUpdateDateModified()
        {
            var todoItem = new TodoItem();

            var initialDateModified = todoItem.DateModified;

            todoItem.UpdateDescription("Walk the dog");

            todoItem.DateModified.Should().BeAfter(initialDateModified);
        }

        [Fact]
        public void MarkCompleteShouldUpdateState()
        {
            var todoItem = new TodoItem();
            
            todoItem.MarkComplete();

            todoItem.IsComplete.Should().BeTrue();
        }
        
        [Fact]
        public void MarkCompleteShouldUpdateDateModified()
        {
            var todoItem = new TodoItem();

            todoItem.MarkComplete();

            todoItem.DateModified.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }
        
        [Fact]
        public void MarkIncompleteShouldUpdateState()
        {
            var todoItem = new TodoItem();
            
            todoItem.MarkIncomplete();

            todoItem.IsComplete.Should().BeFalse();
        }
        
        [Fact]
        public void MarkIncompleteShouldUpdateDateModified()
        {
            var todoItem = new TodoItem();

            todoItem.MarkIncomplete();

            todoItem.DateModified.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }
        
        [Fact]
        public void DeleteShouldSoftDelete()
        {
            var todoItem = new TodoItem();
            
            todoItem.Delete();

            todoItem.IsDeleted.Should().BeTrue();
        }
        
        [Fact]
        public void DeleteShouldUpdateDateModified()
        {
            var todoItem = new TodoItem();

            todoItem.Delete();

            todoItem.DateModified.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}