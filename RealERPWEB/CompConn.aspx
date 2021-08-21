<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompConn.aspx.cs" Inherits="RealERPWEB.CompConn" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">

    <script type="text/javascript">


        function submitform() { document.getElementById("form1").submit(); }
    </script>
</head>
<body>
  

            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
                    <Scripts>
                    </Scripts>

                </asp:ScriptManager>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <div>
                    <div class="container h-100">

                        <div class="col-6 mx-auto">
                            <asp:Panel ID="pnlTop" runat="server">
                                <div class="form-group d-none">
                                    <asp:HiddenField ID="HidnsysID" runat="server" />

                                </div>
                                <div class="form-group mt-5">

                                    <div class="row">
                                        <div class="col-md-3">Expiry Date</div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtExpDate" runat="server" AutoPostBack="True" CssClass="form-control" TabIndex="1" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtExpDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtExpDate"></cc1:CalendarExtender>

                                        </div>
                                    </div>

                                </div>
                                <hr>
                                <div class="form-group">

                                    <div class="row">
                                        <div class="col-md-3">DT Propertis</div>
                                        <div class="col-md-6">

                                            <asp:TextBox ID="txt_CNUMBER" TextMode="Number" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <hr>
                                <div class="form-group">
                                    <legend>DT Propertis Limit</legend>
                                    <div class="row mb-1">
                                        <div class="col-md-3">Limit 1</div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txt_NineFive" TextMode="Number" runat="server" class="form-control"></asp:TextBox>


                                        </div>
                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-md-3">Limit 2</div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txt_NineSix" TextMode="Number" runat="server" class="form-control"></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-md-3">Limit 3</div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txt_NineSeven" TextMode="Number" runat="server" class="form-control"></asp:TextBox>

                                        </div>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-9">
                                        <button type="button" runat="server" id="btnUpdateSqlLimit" onserverclick="btnUpdateSqlLimit_ServerClick" class="btn btn-primary float-right">Update</button>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlbillalrt" runat="server">
                                <div class="mt-5">

                         
                          

                            <div class="form-group">
                                <label for="username">Type Alert Message</label>
                                <textarea rows="5" runat="server" id="txtAltMessage" class="form-control"></textarea>
                            </div>

                            <div class="form-group mb-3">
                                <label>Color Set</label>
                                <div id="xcp-component" class="input-group">
                                    <input type="text" runat="server" id="txtColorCode" class="form-control" />
                                    <span class="input-group-addon"><i></i></span>
                                </div>
                            </div>



                            <div class="form-group">
                                <label for="username">Status</label>
                                <div class="custom-control custom-switch">
                                    <asp:RadioButtonList runat="server" ID="rbtLsit" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="True">Active</asp:ListItem>
                                        <asp:ListItem Value="False">InActive</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                            </div>


                        </div>
                       
                            <button type="button" runat="server" id="btnAlrtMsg" onserverclick="btnAlrtMsg_ServerClick" class="btn btn-primary">Update</button>
                     
                            </asp:Panel>



                            <asp:Panel ID="pnlmsg" runat="server" Visible="false">
                                <div class="alert alert-primary" id="msgBox" runat="server" role="alert">
                                    Data updated successfully
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlDtPropertis" runat="server" Visible="False">
                                <div class="form-group">
                                    <div>
                                        <asp:Label ID="lblDt" runat="server" CssClass="lblTxt lblName" Text="DT Propertis"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDtProperties" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="form-group">
                                    <div>DT Propertis Limit</div>
                                    <div class="col-md-2">
                                       <asp:HiddenField ID="lblHL1" runat="server" />
                                        <asp:Label ID="lblL1" runat="server" CssClass="lblTxt lblName" Text="Limit 1"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtL1" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    </div>
                                      <div class="col-md-2">
                                          <asp:HiddenField ID="lblHL2" runat="server" />
                                        <asp:Label ID="lblL2" runat="server" CssClass="lblTxt lblName" Text="Limit 2"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtL2" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    </div>
                                      <div class="col-md-2">
                                          <asp:HiddenField ID="lblHL3" runat="server" />
                                        <asp:Label ID="lblL3" runat="server" CssClass="lblTxt lblName" Text="Limit 3"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtL3" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <button type="button" runat="server" id="btnDtPropSave" onserverclick="btnDtPropSave_ServerClick" class="btn btn-success">Save</button>
                                </div>
                                
                           </asp:Panel>

                            <asp:Panel ID="pnlAleartMsg" runat="server" Visible="False">

                                <div class="modal-body">

                            <div class="form-group d-none">
                                <asp:TextBox ID="_sqlLnk1" runat="server"></asp:TextBox>
                                <asp:TextBox ID="_sqlLnkuser1" runat="server"></asp:TextBox>
                                <asp:TextBox ID="_sqlLnkpwd1" runat="server"></asp:TextBox>


                            </div>
                            <label runat="server" id="lblmsg" class="btn btn-sm float-right mr-2"></label>

                            <asp:HiddenField ID="hiddDbName" runat="server" />
                            <asp:HiddenField ID="mCompID" runat="server" />
                            <div class="form-group">
                                <label for="username">Master Company</label>
                                <input type="text" class="form-control" runat="server" id="txtMasterComp" readonly>
                            </div>

                            <div class="form-group">
                                <label for="username">Type Alert Message</label>
                                <textarea rows="5" runat="server" id="Textarea1" class="form-control"></textarea>
                            </div>

                            <div class="form-group mb-3">
                                <label>Color Set</label>
                                <div id="xcp-component" class="input-group">
                                    <input type="text" runat="server" id="Text1" class="form-control" />
                                    <span class="input-group-addon"><i></i></span>
                                </div>
                            </div>



                            <div class="form-group">
                                <label for="username">Status</label>
                                <div class="custom-control custom-switch">
                                    <asp:RadioButtonList runat="server" ID="RadioButtonList1" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="True">Active</asp:ListItem>
                                        <asp:ListItem Value="False">InActive</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                            </div>


                        </div>
                                </asp:Panel>





                        </div>


                    </div>
                </div>
               </ContentTemplate>
    </asp:UpdatePanel>
            </form>
     
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
