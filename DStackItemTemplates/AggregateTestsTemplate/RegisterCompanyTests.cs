using Xunit;
using $domain$.Common;
using $domain$.PL.Commands;
using $domain$.PL.Events;
using System;
using System.Threading.Tasks;

namespace $rootnamespace$.$fileinputname$Tests
{
    public class Create$fileinputname$Tests : $fileinputname$AggregateTester
    {
        [Fact]
        public async Task Should_Execute_Command()
        {
            var id = "$fileinputname$s-1";
            var cmd = new Create$fileinputname$() { Id = id, AuditInfo = AuditInfoTestData.CreateDefault(), Name = "Aggregate name" };
            var ev = new $fileinputname$Created() { Id = id, AuditInfo = AuditInfoTestData.CreateDefault(), Name = "Aggregate name" };
       
            Given();
            When(cmd);
            await Expect(ev);
        }

        [Fact]
        public async Task Command_Is_Idempotent()
        {
            var id = "$fileinputname$s-2";
            var cmd = new Create$fileinputname$() { Id = id, AuditInfo = AuditInfoTestData.CreateDefault(), Name = "Aggregate name" };
            var ev = new $fileinputname$Created() { Id = id, AuditInfo = AuditInfoTestData.CreateDefault(), Name = "Aggregate name" };
            Given(ev);
            When(cmd);
            await ExpectNoEvents();
        }

        [Fact]
        public async Task NonIdempotent_Command_Should_Throw_DomainError()
        {
            var id = "$fileinputname$s-3";
            var cmd = new Create$fileinputname$() { Id = id, AuditInfo = AuditInfoTestData.CreateDefault(), Name = "Different aggregate name" };
            var ev = new $fileinputname$Created() { Id = id, AuditInfo = AuditInfoTestData.CreateDefault(), Name = "Aggregate name" };
            
            Given(ev);
            When(cmd);
            await ExpectError("$fileinputname$AlreadyExists");
        }
    }
}