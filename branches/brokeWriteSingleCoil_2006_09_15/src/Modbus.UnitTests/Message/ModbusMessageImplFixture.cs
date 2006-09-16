using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Modbus.Message;

namespace Modbus.UnitTests.Message
{
	[TestFixture]
	public class ModbusMessageImplFixture
	{
		[Test]
		public void ModbusMessageCtorInitializesProperties()
		{
			ModbusMessageImpl messageImpl = new ModbusMessageImpl(5, Modbus.ReadCoils);
			Assert.AreEqual(5, messageImpl.SlaveAddress);
			Assert.AreEqual(Modbus.ReadCoils, messageImpl.FunctionCode);
		}

		[Test]
		public void Initialize()
		{
			ModbusMessageImpl messageImpl = new ModbusMessageImpl();
			messageImpl.Initialize(new byte[] { 1, 2, 9, 9, 9, 9 });
			Assert.AreEqual(1, messageImpl.SlaveAddress);
			Assert.AreEqual(2, messageImpl.FunctionCode);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ChecckInitializeFrameNull()
		{
			ModbusMessageImpl messageImpl = new ModbusMessageImpl();
			messageImpl.Initialize(null);
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void InitializeInvalidFrame()
		{
			ModbusMessageImpl messageImpl = new ModbusMessageImpl();
			messageImpl.Initialize(new byte[] { 1 });
		}

		[Test]
		public void ProtocolDataUnit()
		{
			ModbusMessageImpl messageImpl = new ModbusMessageImpl(11, Modbus.ReadCoils);
			byte[] expectedResult = new byte[] { Modbus.ReadCoils };
			Assert.AreEqual(expectedResult, messageImpl.ProtocolDataUnit);
		}

		[Test]
		public void MessageFrame()
		{
			ModbusMessageImpl messageImpl = new ModbusMessageImpl(11, Modbus.ReadHoldingRegisters);
			byte[] expectedMessageFrame = new byte[] { 11, Modbus.ReadHoldingRegisters };
			Assert.AreEqual(expectedMessageFrame, messageImpl.MessageFrame);
		}		
	}
}
