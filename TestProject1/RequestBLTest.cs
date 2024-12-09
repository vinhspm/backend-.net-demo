using DataAccess;
using Moq;

namespace Domain.Test
{
    public class RequestBLTest
    {
        private RequestBL _requestBL;
        private Mock<IRequestDL> _requestDLMock;
        private Mock<IEmployeeDL> _emplMock;
        private Mock<IDepartmentBL> _departmentMock;
        private Mock<IPositionDL> _positionBLMock;
        private Mock<IRequestDetailBL> _requestDetailBLMock;

        public RequestBLTest()
        {
            _requestDLMock = new Mock<IRequestDL>();
            _emplMock = new Mock<IEmployeeDL>();
            _departmentMock = new Mock<IDepartmentBL>();
            _positionBLMock = new Mock<IPositionDL>();
            _requestDetailBLMock = new Mock<IRequestDetailBL>();

            _requestBL = new RequestBL(_requestDLMock.Object, _emplMock.Object, _departmentMock.Object, _positionBLMock.Object, _requestDetailBLMock.Object);
        }

        [Fact]
        public void MultipleDelete_Should_Call_DL()
        {
            var guids = new List<Guid>();
            guids.Add(Guid.NewGuid());
            guids.Add(Guid.NewGuid());

            _requestBL.MultipleDelete(guids);

            _requestDLMock.Verify(repo => repo.MultipleDelete(It.Is<List<Guid>>(request => request.Any())), Times.Once);
        }
    }
}